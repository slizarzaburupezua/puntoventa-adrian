using AutoMapper;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.QuestPDFLibrary;
using Serilog;
using GE = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using GS = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class ParametrosGeneralesService : IParametrosGeneralesService
    {
        private readonly IMapper _mapper;
        private readonly IParametrosGeneralesRepository _parametrosGeneralesRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IToolService _toolService;
        private readonly IVentaQuestService _ventaQuestService;
        private readonly IParametroRepository _parametroRepository;

        public ParametrosGeneralesService(IMapper mapper,
                              IParametrosGeneralesRepository parametrosGeneralesRepository,
                              ICloudinaryService cloudinaryService,
                              IUsuarioRepository usuarioRepository,
                              IToolService toolService,
                              IVentaQuestService ventaQuestService,
                              IParametroRepository parametroRepository
                              )
        {
            _mapper = mapper;
            _parametrosGeneralesRepository = parametrosGeneralesRepository;
            _usuarioRepository = usuarioRepository;
            _cloudinaryService = cloudinaryService;
            _toolService = toolService;
            _ventaQuestService = ventaQuestService;
            _parametroRepository = parametroRepository;
        }

        public async Task<List<RolDTO>> GetAllRolAsync()
        {
            return _mapper.Map<List<RolDTO>>(await _parametrosGeneralesRepository.GetAllRolAsync());
        }

        public async Task<List<MonedaDTO>> GetAllMonedaAsync()
        {
            return _mapper.Map<List<MonedaDTO>>(await _parametrosGeneralesRepository.GetAllMonedaAsync());
        }

        public async Task<List<TipoDocumentoDTO>> GetAllTipoDocumentoAsync()
        {
            return _mapper.Map<List<TipoDocumentoDTO>>(await _parametrosGeneralesRepository.GetAllTipoDocumentoAsync());
        }

        public async Task<List<GeneroDTO>> GetAllGeneroAsync()
        {
            return _mapper.Map<List<GeneroDTO>>(await _parametrosGeneralesRepository.GetAllGeneroAsync());
        }

        public async Task<NegocioDTO> GetNegocioAsync()
        {
            return _mapper.Map<NegocioDTO>(await _parametrosGeneralesRepository.GetNegocioAsync());
        }

        public async Task<ResponseDTO> UpdateNegocioAsync(ActualizarNegocioRequest request)
        {
            Log.Information(LogMessages.Negocio.UpdateNegocioAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Negocio.UpdateNegocioAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var negocioEntity = await _parametrosGeneralesRepository.GetNegocioAsync();

            negocioEntity.ID_MONEDA = request.IdMoneda;
            negocioEntity.RAZON_SOCIAL = request.RazonSocial.Trim() ?? string.Empty;
            negocioEntity.RUC = request.Ruc.Trim() ?? string.Empty;
            negocioEntity.DIRECCION = request.Direccion.Trim() ?? string.Empty;
            negocioEntity.CELULAR = request.Celular.Trim() ?? string.Empty;
            negocioEntity.CORREO_ELECTRONICO = request.Correo.Trim() ?? string.Empty;
            negocioEntity.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            negocioEntity.COLOR_BOLETA_FACTURA = request.ColorBoleta;
            negocioEntity.FORMATO_IMPRESION = request.CodFormatoImpresion;

            if (request.Foto != negocioEntity.URLFOTO)
                await GestionarFoto(negocioEntity, request.Foto, request.NombreArchivo);

            await _parametrosGeneralesRepository.UpdateNegocioAsync(negocioEntity);

            Log.Information(LogMessages.Negocio.UpdateNegocioAsync.Finish, request.IdUsuarioGuid);

            return new ResponseDTO
            {
                Success = Flags.SuccessTransaction,
                TitleMessage = GS.SuccessTitleTransaction,
                Message = ResponseMessages.Negocio.SuccessInformacionCambiada
            };
        }

        public async Task<ResponseDTO> VistaPreviaBoletaFacturaAsync(ActualizarNegocioRequest request)
        {
            Log.Information(LogMessages.Parametro.VistaPreviaBoletaFacturaAsync.Initial);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Parametro.VistaPreviaBoletaFacturaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var moneda = await _parametrosGeneralesRepository.GetMonedaByCodigoAsync(request.CodMoneda);

            var negocio = new Negocio
            {
                RAZON_SOCIAL = request.RazonSocial,
                RUC = request.Ruc,
                DIRECCION = request.Direccion,
                CELULAR = request.Celular,
                CORREO_ELECTRONICO = request.Correo,
                COLOR_BOLETA_FACTURA = request.ColorBoleta,
                FORMATO_IMPRESION = request.CodFormatoImpresion,
                MONEDA = moneda,
                URLFOTO = "https://res.cloudinary.com/dvzkgpiv3/image/upload/v1753766493/TU_LOGO_1_kz0gkg.png"
            };

            var filtroBoletaFactura = new FiltroGenerarBoletaFactura
            {
                InformacionNegocio = negocio,
                InformacionCliente = GenerarMockupCliente(),
                Venta = GenerarMockupVenta(),
                LstDetalleVenta = GenerarMockupDetalleVenta()
            };

            var (fileName, base64File) = await _ventaQuestService.GenerarBoletaFacturaAsync(filtroBoletaFactura);

            var boletaFacturaResponse = await _cloudinaryService.UploadFileAsync(fileName, base64File, Parametros.FolderPreviewBoletasFacturas);

            return new ResponseDTO
            {
                Success = Flags.SuccessTransaction,
                Value = boletaFacturaResponse.SecureURL
            };
        }
         
        #region PRIVATE METHODS

        private async Task GestionarFoto(Negocio negocio, string nuevaFotoBase64, string nombreArchivoFoto)
        {
            if (string.IsNullOrEmpty(nuevaFotoBase64))
            {
                if (!string.IsNullOrEmpty(negocio.URLFOTO))
                {
                    await _cloudinaryService.DeleteImageAsync(negocio.ID_FOTO);
                    negocio.ID_FOTO = string.Empty;
                    negocio.URLFOTO = string.Empty;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(negocio.URLFOTO) && _toolService.EsBase64(nuevaFotoBase64))
                {
                    var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderNegocio);
                    negocio.ID_FOTO = fotoResponse.PublicId;
                    negocio.URLFOTO = fotoResponse.SecureURL;
                }
                else
                {
                    if (nuevaFotoBase64 != negocio.URLFOTO && !string.IsNullOrEmpty(negocio.URLFOTO))
                    {
                        await _cloudinaryService.DeleteImageAsync(negocio.ID_FOTO);

                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderNegocio);
                        negocio.ID_FOTO = fotoResponse.PublicId;
                        negocio.URLFOTO = fotoResponse.SecureURL;
                    }
                    else
                    {
                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderNegocio);
                        negocio.ID_FOTO = fotoResponse.PublicId;
                        negocio.URLFOTO = fotoResponse.SecureURL;
                    }

                }
            }
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

        private async Task<int> GetIdUsuarioByGuid(Guid idUsuario)
        {
            return await _usuarioRepository.GetIdUsuarioByGuid(idUsuario);
        }

        private Venta GenerarMockupVenta()
        {
            return new Venta()
            {
                NUMERO_VENTA = "001-000001",
                FECHA_VENTA = DateTime.Now,
                PRECIO_TOTAL = 250.75m,
                NOTA_ADICIONAL = "El cliente hará el pago contraentrega",
            };
        }

        private Cliente GenerarMockupCliente()
        {
            return new Cliente()
            {
                NUMERO_DOCUMENTO = "12345678",
                NOMBRES = "Juan",
                APELLIDOS = "Pérez",
                CORREO_ELECTRONICO = "juan.perez@example.com",
                CELULAR = "987654321",
                DIRECCION = "Av. Siempre Viva 742",
            };
        }

        private List<DetalleVenta> GenerarMockupDetalleVenta()
        {
            var LstDetalleVenta = new List<DetalleVenta>
            {
                new DetalleVenta
                {
                    NOMBRE_PRODUCTO = "Camiseta Negra",
                    CANTIDAD = 2,
                    PRECIO_VENTA = 40.00m,
                    PRECIO_TOTAL = 80.00m,
                },
                new DetalleVenta
                {
                    NOMBRE_PRODUCTO = "Pantalón Azul",
                    CANTIDAD = 1,
                    PRECIO_VENTA = 70.75m,
                    PRECIO_TOTAL = 70.75m,
                },
                new DetalleVenta
                {
                    NOMBRE_PRODUCTO = "Guantes Rojos",
                    CANTIDAD = 2,
                    PRECIO_VENTA = 40.00m,
                    PRECIO_TOTAL = 80.00m,
                },
                new DetalleVenta
                {
                    NOMBRE_PRODUCTO = "Zapatillas Negras",
                    CANTIDAD = 1,
                    PRECIO_VENTA = 70.75m,
                    PRECIO_TOTAL = 70.75m,
                }
            };

            return LstDetalleVenta;
        }

        #endregion
    }
}
