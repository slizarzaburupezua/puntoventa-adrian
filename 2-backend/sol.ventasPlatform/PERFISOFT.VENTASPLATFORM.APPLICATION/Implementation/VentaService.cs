using AutoMapper;
using PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.QuestPDFLibrary;
using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;
using Serilog;
using GE = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using GS = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class VentaService : IVentaService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IVentaNumeroCorrelativoRepository _ventaNumeroCorrelativoRepository;
        private readonly IVentaQuestService _ventaQuestService;
        private readonly IVentaRepository _ventaRepository;
        private readonly IDetalleVentaRepository _detalleVentaRepository;
        private readonly IParametrosGeneralesRepository _parametroRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public VentaService(IMapper mapper,
                            IVentaNumeroCorrelativoRepository ventaNumeroCorrelativoRepository,
                            IVentaRepository ventaRepository,
                            IVentaQuestService ventaQuestService,
                            IDetalleVentaRepository detalleVentaRepository,
                            IUnitOfWork unitOfWork,
                            IUsuarioRepository usuarioRepository,
                            IProductoRepository productoRepository,
                            ICloudinaryService cloudinaryService,
                            IParametrosGeneralesRepository parametrosGeneralesRepository,
                            IClienteRepository clienteRepository,
                            IEmailService emailService
            )
        {
            _mapper = mapper;
            _ventaNumeroCorrelativoRepository = ventaNumeroCorrelativoRepository;
            _ventaRepository = ventaRepository;
            _detalleVentaRepository = detalleVentaRepository;
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;
            _ventaQuestService = ventaQuestService;
            _cloudinaryService = cloudinaryService;
            _parametroRepository = parametrosGeneralesRepository;
            _emailService = emailService;
        }

        public async Task<IList<VentaDTO>> GetAllByFilterAsync(ObtenerVentaRequest request)
        {
            return _mapper.Map<IList<VentaDTO>>(await _ventaRepository.SelectAllByFilterAsync(_mapper.Map<FiltroObtenerVenta>(request)));
        }

        public async Task<IList<UsuarioDTO>> GetAllUsuariosAsync()
        {
            return _mapper.Map<IList<UsuarioDTO>>(await _usuarioRepository.SelectAllAsync());
        }

        public async Task<ResponseDTO> InsertAsync(RegistrarVentaRequest request)
        {
            Log.Information(LogMessages.Venta.InsertAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Venta.InsertAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var productos = await _productoRepository.SelectByIdsAsync(request.LstDetalleVenta
                .Select(x => x.IdProducto)
                .ToList());

            if (productos.Any(p => p.STOCK < request.LstDetalleVenta.First(d => d.IdProducto == p.ID).Cantidad))
            {
                Log.Warning(LogMessages.Venta.InsertAsync.ErrorStock);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var correlativo = await _ventaNumeroCorrelativoRepository.SelectNextCorrelativoAsync(Parametros.SerieVenta);
                var numeroVenta = GenerarNumeroVenta(correlativo.NUMERO, correlativo.SERIE);
                var filtroRegistroVenta = ObtenerFiltroRegistrarVenta(request, idUsuario, numeroVenta);

                var ventaGenerada = await _ventaRepository.InsertAsync(_mapper.Map<Venta>(filtroRegistroVenta));

                foreach (var detalle in request.LstDetalleVenta)
                {
                    var producto = productos.FirstOrDefault(p => p.ID == detalle.IdProducto);
                    producto.STOCK -= detalle.Cantidad;
                    producto.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdRegistro);
                }

                await _productoRepository.UpdateAsync(productos);

                var filtroRegistroDetalleVenta = ObtenerFiltroRegistrarDetalleVenta(request.LstDetalleVenta, ventaGenerada.ID, request.DestinationTimeZoneIdRegistro, request.FechaRegistroVenta);

                var detalleVenta = await _detalleVentaRepository.InsertAsync(_mapper.Map<List<DetalleVenta>>(filtroRegistroDetalleVenta));

                await _ventaNumeroCorrelativoRepository.UpdateCorrelativoAsync(correlativo.SERIE);

                var negocio = await _parametroRepository.GetNegocioAsync();

                var filtroBoletaFactura = new FiltroGenerarBoletaFactura();

                filtroBoletaFactura.InformacionNegocio = negocio;
                filtroBoletaFactura.InformacionCliente = ventaGenerada.CLIENTE;
                filtroBoletaFactura.Venta = ventaGenerada;
                filtroBoletaFactura.LstDetalleVenta = detalleVenta;

                var (fileName, base64File) = await _ventaQuestService.GenerarBoletaFacturaAsync(filtroBoletaFactura);

                var boletaFacturaResponse = await _cloudinaryService.UploadFileAsync(fileName, base64File, Parametros.FolderBoletasFactura);

                if (request.FlgEnviarComprobante && !string.IsNullOrEmpty(ventaGenerada.CLIENTE?.CORREO_ELECTRONICO))
                {
                    var asunto = "Boleta de Venta - " + ventaGenerada.NUMERO_VENTA;
                    var mensaje = $@"Estimado/a {ventaGenerada.CLIENTE.NOMBRES},<br/><br/>Gracias por su compra. Adjunto encontrará su comprobante de venta.<br/><br/>Saludos,<br/>El equipo de ventas.";

                    byte[] pdfBytes = Convert.FromBase64String(base64File);

                    Log.Information("Enviando comprobante al correo: {Correo} con asunto: {Asunto}", ventaGenerada.CLIENTE.CORREO_ELECTRONICO, asunto);

                    await _emailService.SendWithAttachmentAsync(
                        ventaGenerada.CLIENTE.CORREO_ELECTRONICO,
                        asunto,
                        mensaje,
                        $"{ventaGenerada.NUMERO_VENTA}.pdf",
                        pdfBytes
                    );

                    Log.Information("Comprobante enviado exitosamente al correo: {Correo}", ventaGenerada.CLIENTE.CORREO_ELECTRONICO);
                }

                ventaGenerada.ID_BOLETAFACTURA = boletaFacturaResponse.PublicId;
                ventaGenerada.URLBOLETAFACTURA = boletaFacturaResponse.SecureURL;
                ventaGenerada.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdRegistro);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return CreateSuccessResponse(ventaGenerada.ID, string.Format(ResponseMessages.Venta.SuccessVenta, ventaGenerada.NUMERO_VENTA), boletaFacturaResponse.SecureURL);
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.Venta.InsertAsync.Error + ex.Message.ToString());
                await _unitOfWork.RollbackTransactionAsync();
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }
        }

        public async Task<ResponseDTO> AnulaAsync(AnularVentaRequest request)
        {
            Log.Information(LogMessages.Venta.AnulaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Venta.AnulaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var ventaEntity = await _ventaRepository.SelectByIdAsync(request.Id);

            if (ventaEntity is null)
            {
                Log.Error(LogMessages.Venta.AnulaAsync.VentaNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            Log.Information("Anulando venta con ID: {VentaId}, Número: {NumeroVenta}", ventaEntity.ID, ventaEntity.NUMERO_VENTA);

            ventaEntity.ESTADO = Flags.Deshabilitar;
            ventaEntity.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            ventaEntity.MOTIVO_ANULACION = string.Empty;

            if (request.RecuperarStock)
            {
                Log.Information("Recuperación de stock habilitada para venta ID: {VentaId}", ventaEntity.ID);

                var detallesVenta = await _detalleVentaRepository.SelectDetalleAsync(ventaEntity.ID);
                var productosParaActualizar = new List<Producto>();

                foreach (var detalle in detallesVenta)
                {
                    var producto = await _productoRepository.SelectByIdAsync(detalle.IdProducto);
                    if (producto != null)
                    {
                        var stockAnterior = producto.STOCK;
                        producto.STOCK += detalle.Cantidad;
                        productosParaActualizar.Add(producto);

                        Log.Information("Producto ID: {ProductoId} - Stock actualizado de {StockAnterior} a {StockNuevo}",
                            producto.ID, stockAnterior, producto.STOCK);
                    }
                    else
                    {
                        Log.Warning("Producto no encontrado: ID {ProductoId} - no se actualizó stock", detalle.IdProducto);
                    }
                }

                if (productosParaActualizar.Any())
                {
                    Log.Information("Actualizando stock para {CantidadProductos} productos", productosParaActualizar.Count);
                    await _productoRepository.UpdateAsync(productosParaActualizar);
                }
                else
                {
                    Log.Warning("No se encontraron productos para actualizar stock en venta ID: {VentaId}", ventaEntity.ID);
                }
            }

            Log.Information("Guardando cambios de anulación de venta ID: {VentaId}", ventaEntity.ID);

            await _ventaRepository.UpdateAsync(ventaEntity);
            await _unitOfWork.SaveAsync();

            Log.Information(LogMessages.Venta.AnulaAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(
                ventaEntity.ID,
                string.Format(ResponseMessages.Venta.SuccessAnulacionVenta, ventaEntity.NUMERO_VENTA),
                request.RecuperarStock ? "Stock de productos recuperado correctamente." : string.Empty
            );
        }

        #region PRIVATE METHODS

        private FiltroRegistrarVenta ObtenerFiltroRegistrarVenta(RegistrarVentaRequest request, int idUsuarioRegistro, string numeroVenta)
        {
            var precioTotal = request.LstDetalleVenta.Sum(d => d.PrecioVenta * d.Cantidad);

            return new FiltroRegistrarVenta
            {
                IdUsuario = idUsuarioRegistro,
                IdCliente = request.IdCliente,
                NumeroVenta = numeroVenta,
                PrecioTotal = precioTotal,
                NotaAdicional = request.NotaAdicional?.Trim(),
                FechaVenta = request.FechaRegistroVenta.ConvertDateTimeClient(request.DestinationTimeZoneIdRegistro),
                Estado = Flags.Habilitar,
                Activo = Flags.Activar,
                FechaRegistro = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdRegistro),
            };
        }

        private List<FiltroRegistrarDetalleVenta> ObtenerFiltroRegistrarDetalleVenta(List<RegistrarDetalleVentaRequest> request, int idVenta, string timeZone, DateTime fechaVenta)
        {
            return request.Select(detalle => new FiltroRegistrarDetalleVenta
            {
                IdVenta = idVenta,
                IdProducto = detalle.IdProducto,
                UrlFotoProducto = detalle.UrlFotoProducto,
                NombreProducto = detalle.NombreProducto,
                ColorProducto = detalle.ColorProducto,
                NombreCategoria = detalle.NombreCategoria,
                ColorCategoria = detalle.ColorCategoria,
                NombreMarca = detalle.NombreMarca,
                ColorMarca = detalle.ColorMarca,
                Cantidad = detalle.Cantidad,
                PrecioCompra = detalle.PrecioCompra,
                PrecioVenta = detalle.PrecioVenta,
                PrecioTotal = detalle.Cantidad * detalle.PrecioVenta,
                Estado = Flags.Habilitar,
                Activo = Flags.Activar,
                FechaRegistro = fechaVenta.ConvertDateTimeClient(timeZone)
            }).ToList();
        }

        private string GenerarNumeroVenta(int numero, string serie)
        {
            var nuevoCorrelativo = numero + Numeracion.Uno;
            var numeroVenta = $"{serie}-{nuevoCorrelativo:D6}";

            return numeroVenta;
        }

        private ResponseDTO CreateErrorEntityResponse(int idEntity)
        {
            return new ResponseDTO
            {
                Id = idEntity,
                Code = ErrorCodigo.Error,
                Success = Flags.ErrorTransaction,
                TitleMessage = GE.ErrorTitleTransaction,
                Message = GE.ErrorTransaction
            };
        }

        private ResponseDTO CreateErrorUserResponse(Guid idUsuario)
        {
            return new ResponseDTO
            {
                IdUsuario = idUsuario,
                Code = ErrorCodigo.Error,
                Success = Flags.ErrorTransaction,
                TitleMessage = GE.ErrorTitleTransaction,
                Message = GE.ErrorTransaction
            };
        }

        private ResponseDTO CreateSuccessResponse(int id, string message, string value)
        {
            return new ResponseDTO
            {
                Id = id,
                Success = Flags.SuccessTransaction,
                TitleMessage = GS.SuccessTitleTransaction,
                Message = message,
                Value = value
            };
        }

        private async Task<int> GetIdUsuarioByGuid(Guid idUsuario)
        {
            return await _usuarioRepository.GetIdUsuarioByGuid(idUsuario);
        }

        #endregion
    }
}
