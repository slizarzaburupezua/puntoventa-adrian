using AutoMapper;
using Microsoft.AspNetCore.Http;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Medida.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService;
using Serilog;
using GE = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using GS = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class InventarioService : IInventarioService
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMedidaRepository _medidaRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IToolService _toolService;

        public InventarioService(IMapper mapper,
                              ICategoriaRepository categoriaRepository,
                              IMedidaRepository medidaRepository,
                              IUsuarioRepository usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IProductoRepository productoRepository,
            IMarcaRepository marcaRepository,
            ICloudinaryService cloudinaryService,
            IToolService toolService
            )
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
            _medidaRepository = medidaRepository;
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;
            _marcaRepository = marcaRepository;
            _cloudinaryService = cloudinaryService;
            _toolService = toolService;
        }

        #region MEDIDA

        public async Task<IList<MedidaDTO>> GetAllMedidaAsync(Guid idUsuarioGuid)
        {
            return _mapper.Map<List<MedidaDTO>>(await _medidaRepository.SelectAllAsync());
        }

        #endregion

        #region CATEGORÍA

        public async Task<IList<CategoriaDTO>> GetAllCategoriaByFilterAsync(ObtenerCategoriaRequest request)
        {
            return _mapper.Map<List<CategoriaDTO>>(await _categoriaRepository.SelectAllByNameAsync(request.Nombre));
        }

        public async Task<IList<CategoriaDTO>> GetAllCategoriasForComboBoxAsync()
        {
            return _mapper.Map<List<CategoriaDTO>>(await _categoriaRepository.SelectAllForComboBoxAsync());
        }

        public async Task<ResponseDTO> InsertCategoriaAsync(RegistrarCategoriaRequest request)
        {
            Log.Information(LogMessages.InsertCategoriaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.InsertCategoriaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var insertCategoriaResponse = _mapper.Map<ResponseDTO>(await _categoriaRepository.InsertAsync(_mapper.Map<Categoria>(request)));

            Log.Information(LogMessages.InsertCategoriaAsync.Finish, request.IdUsuarioGuid);

            return insertCategoriaResponse;
        }

        public async Task<ResponseDTO> UpdateCategoriaAsync(ActualizarCategoriaRequest request)
        {
            Log.Information(LogMessages.UpdateCategoriaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.UpdateCategoriaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var categoriaEntity = await _categoriaRepository.SelectByIdAsync(request.Id);

            if (categoriaEntity is null)
            {
                Log.Error(LogMessages.UpdateCategoriaAsync.CategoriaNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            categoriaEntity.NOMBRE = request.Nombre.Trim();
            categoriaEntity.ID_MEDIDA = request.Id_Medida;
            categoriaEntity.DESCRIPCION = request.Descripcion.Trim() ?? string.Empty;
            categoriaEntity.COLOR = request.Color;
            categoriaEntity.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            categoriaEntity.ACTIVO = request.Activo;

            var updateCategoriaResponse = await _categoriaRepository.UpdateAsync(categoriaEntity);

            Log.Information(LogMessages.UpdateCategoriaAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(updateCategoriaResponse.ID);
        }

        public async Task<ResponseDTO> DeleteCategoriaAsync(EliminarCategoriaRequest request)
        {
            Log.Information(LogMessages.DeleteCategoriaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.DeleteCategoriaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var categoriaEntity = await _categoriaRepository.SelectByIdAsync(request.Id);

            if (categoriaEntity is null)
            {
                Log.Error(LogMessages.DeleteCategoriaAsync.CategoriaNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            if (await _categoriaRepository.ExistProductoCategoriaAsync(request.Id, idUsuario))
            {
                Log.Warning(LogMessages.DeleteCategoriaAsync.ExisteProductosAsociados);
                return CreateWarningResponse(ResponseMessages.Inventario.Categoria.ExistProductosCategoria);
            }

            categoriaEntity.ESTADO = Flags.Deshabilitar;
            categoriaEntity.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            categoriaEntity.MOTIVO_ANULACION = request.MotivoAnulacion ?? string.Empty;

            var modifyCategoriaEntity = await _categoriaRepository.UpdateAsync(categoriaEntity);

            Log.Information(LogMessages.DeleteCategoriaAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(modifyCategoriaEntity.ID);
        }

        public async Task<ResponseDTO> UpdateActivoCategoriaAsync(ActualizarActivoCategoriaRequest request)
        {
            Log.Information(LogMessages.UpdateActivoCategoriaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.UpdateActivoCategoriaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var categoraEntity = await _categoriaRepository.SelectByIdAsync(request.Id);

            if (categoraEntity is null)
            {
                Log.Error(LogMessages.UpdateActivoCategoriaAsync.CategoriaNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            await _categoriaRepository.UpdateActivoAsync(categoraEntity.ID, request.Activo);

            Log.Information(LogMessages.UpdateActivoCategoriaAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(categoraEntity.ID);
        }

        #endregion

        #region MARCA

        public async Task<IList<MarcaDTO>> GetAllMarcaByFilterAsync(ObtenerMarcaRequest request)
        {
            return _mapper.Map<List<MarcaDTO>>(await _marcaRepository.SelectAllByNameAsync(request.Nombre));
        }

        public async Task<IList<MarcaDTO>> GetAllMarcasForComboBoxAsync()
        {
            return _mapper.Map<List<MarcaDTO>>(await _marcaRepository.SelectAllForComboBoxAsync());
        }

        public async Task<ResponseDTO> InsertMarcaAsync(RegistrarMarcaRequest request)
        {
            Log.Information(LogMessages.InsertMarcaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.InsertMarcaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var response = _mapper.Map<ResponseDTO>(await _marcaRepository.InsertAsync(_mapper.Map<Marca>(request)));

            Log.Information(LogMessages.InsertMarcaAsync.Finish, request.IdUsuarioGuid);

            return response;
        }

        public async Task<ResponseDTO> UpdateMarcaAsync(ActualizarMarcaRequest request)
        {
            Log.Information(LogMessages.UpdateMarcaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.UpdateMarcaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var marcaEntity = await _marcaRepository.SelectByIdAsync(request.Id);

            if (marcaEntity is null)
            {
                Log.Error(LogMessages.UpdateMarcaAsync.MarcaNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            marcaEntity.NOMBRE = request.Nombre.Trim();
            marcaEntity.DESCRIPCION = request.Descripcion.Trim() ?? string.Empty;
            marcaEntity.COLOR = request.Color;
            marcaEntity.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            marcaEntity.ACTIVO = request.Activo;

            var updateMarca = await _marcaRepository.UpdateAsync(marcaEntity);

            Log.Information(LogMessages.UpdateMarcaAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(updateMarca.ID);
        }

        public async Task<ResponseDTO> DeleteMarcaAsync(EliminarMarcaRequest request)
        {
            Log.Information(LogMessages.DeleteMarcaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.DeleteMarcaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var marcaEntity = await _marcaRepository.SelectByIdAsync(request.Id);

            if (marcaEntity is null)
            {
                Log.Error(LogMessages.DeleteMarcaAsync.MarcaNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            if (await _marcaRepository.ExistProductoMarcaAsync(request.Id, idUsuario))
            {
                Log.Warning(LogMessages.DeleteMarcaAsync.ExisteProductosAsociados);
                return CreateWarningResponse(ResponseMessages.Inventario.Marca.ExistProductosMarca);
            }

            marcaEntity.ESTADO = Flags.Deshabilitar;
            marcaEntity.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            marcaEntity.MOTIVO_ANULACION = request.MotivoAnulacion ?? string.Empty;

            var modifyMarcaEntity = await _marcaRepository.UpdateAsync(marcaEntity);

            Log.Information(LogMessages.DeleteMarcaAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(modifyMarcaEntity.ID);
        }

        public async Task<ResponseDTO> UpdateActivoMarcaAsync(ActualizarActivoMarcaRequest request)
        {
            Log.Information(LogMessages.UpdateActivoMarcaAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.UpdateActivoMarcaAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var marcaEntity = await _marcaRepository.SelectByIdAsync(request.Id);

            if (marcaEntity is null)
            {
                Log.Error(LogMessages.UpdateActivoMarcaAsync.MarcaNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            await _marcaRepository.UpdateActivoAsync(marcaEntity.ID, request.Activo);

            Log.Information(LogMessages.UpdateActivoMarcaAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(marcaEntity.ID);
        }

        #endregion

        #region PRODUCTO

        public async Task<List<ProductoDTO>> GetProductoByCodeAsync(ObtenerProductoPorCodigoRequest request)
        {
            return _mapper.Map<List<ProductoDTO>>(await _productoRepository.SelectAllByCodeAsync(request.Parametro));
        }

        public async Task<IList<CategoriaConConteoDTO>> GetCategoriesWithProductsCountAsync()
        {
            return _mapper.Map<IList<CategoriaConConteoDTO>>(await _productoRepository.SelectCategoriesWithProductsCountAsync());
        }

        public async Task<IList<ProductoDTO>> GetAllProductsByCategoryAsync(int idCategory)
        {
            return _mapper.Map<IList<ProductoDTO>>(await _productoRepository.SelectByCategoryAsync(idCategory));
        }

        public async Task<IList<ProductoDTO>> GetAllProductoByFilterAsync(ObtenerProductoRequest request)
        {
            return _mapper.Map<List<ProductoDTO>>(await _productoRepository.SelectAllByFilterAsync(_mapper.Map<FiltroConsultaProducto>(request)));
        }

        public async Task<IList<ProductoDTO>> GetAllProductoForComboBoxAsync()
        {
            return _mapper.Map<List<ProductoDTO>>(await _productoRepository.SelectAllForComboBoxAsync());
        }

        public async Task<ResponseDTO> InsertProductoAsync(RegistrarProductoRequest request)
        {
            Log.Information(LogMessages.Producto.InsertAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Producto.InsertAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            if (await _productoRepository.ExistCodigoProductoAsync(request.Codigo.Trim()))
            {
                Log.Warning(LogMessages.Producto.InsertAsync.CodigoExiste, request.Codigo);
                return CreateResponse(ErrorCodigo.Advertencia, Flags.WarningTransaction, string.Format(ResponseMessages.Inventario.Producto.ExistCodigoAsync.CodigoExiste, request.Codigo), string.Empty);
            }

            if (await _productoRepository.ExistNombreProductoAsync(request.Nombre.Trim()))
            {
                Log.Warning(LogMessages.Producto.InsertAsync.NombreExiste, request.Nombre);
                return CreateResponse(ErrorCodigo.Advertencia, Flags.WarningTransaction, string.Format(ResponseMessages.Inventario.Producto.ExistNombreProductoAsync.NombreExiste, request.Nombre), string.Empty);
            }

            if (!string.IsNullOrEmpty(request.FotoBase64))
            {
                Log.Information(LogMessages.Producto.InsertAsync.SubiendoFoto, request.IdUsuarioGuid);
                var fotoResponse = await _cloudinaryService.UploadImageAsync(request.NombreArchivo, request.FotoBase64, Parametros.FolderProductos);
                request.IdFoto = fotoResponse.PublicId;
                request.UrlFoto = fotoResponse.SecureURL;
                Log.Information(LogMessages.Producto.InsertAsync.FotoCargadaCorrectamente, fotoResponse.PublicId);
            }

            var response = _mapper.Map<ResponseDTO>(await _productoRepository.InsertAsync(_mapper.Map<Producto>(request)));

            Log.Information(LogMessages.Producto.InsertAsync.Finish, request.IdUsuarioGuid);

            return response;
        }

        public async Task<ResponseDTO> UpdateProductoAsync(ActualizarProductoRequest request)
        {
            Log.Information(LogMessages.Producto.UpdateAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Producto.UpdateAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var productoEntity = await _productoRepository.SelectByIdAsync(request.Id);

            if (productoEntity is null)
            {
                Log.Error(LogMessages.Producto.UpdateAsync.ProductoNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            productoEntity.ID_MARCA = request.Id_Marca;
            productoEntity.ID_CATEGORIA = request.Id_Categoria;
            productoEntity.NOMBRE = request.Nombre.Trim();
            productoEntity.DESCRIPCION = request.Descripcion.Trim() ?? string.Empty;
            productoEntity.COLOR = request.Color.Trim();
            productoEntity.PRECIO_COMPRA = request.PrecioCompra;
            productoEntity.PRECIO_VENTA = request.PrecioVenta;
            productoEntity.STOCK = request.Stock;
            productoEntity.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);

            if (request.Foto != productoEntity.URLFOTO)
                await GestionarFoto(productoEntity, request.Foto, request.NombreArchivo);

            var updateProducto = await _productoRepository.UpdateAsync(productoEntity);

            Log.Information(LogMessages.Producto.UpdateAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(updateProducto.ID);
        }

        public async Task<ResponseDTO> DeleteProductoAsync(EliminarProductoRequest request)
        {
            Log.Information(LogMessages.Producto.DeleteAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Producto.DeleteAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var productoEntity = await _productoRepository.SelectByIdAsync(request.Id);

            if (productoEntity is null)
            {
                Log.Error(LogMessages.Producto.DeleteAsync.ProductoNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            productoEntity.ESTADO = Flags.Deshabilitar;
            productoEntity.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            productoEntity.MOTIVO_ANULACION = request.MotivoAnulacion ?? string.Empty;

            var modifyGastoCategoriaEntity = await _productoRepository.UpdateAsync(productoEntity);

            Log.Information(LogMessages.Producto.DeleteAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(modifyGastoCategoriaEntity.ID);
        }

        public async Task<ResponseDTO> UpdateActivoProductoAsync(ActualizarActivoProductoRequest request)
        {
            Log.Information(LogMessages.Producto.UpdateActivoAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Producto.UpdateActivoAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var productoEntity = await _productoRepository.SelectByIdAsync(request.Id);

            if (productoEntity is null)
            {
                Log.Error(LogMessages.Producto.UpdateActivoAsync.ProductoNoExiste, request.Id);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            await _productoRepository.UpdateActivoAsync(productoEntity.ID, request.Activo);

            Log.Information(LogMessages.Producto.UpdateActivoAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(productoEntity.ID);
        }

        #endregion

        #region PRIVATE METHODS

        private async Task GestionarFoto(Producto producto, string nuevaFotoBase64, string nombreArchivoFoto)
        {
            if (string.IsNullOrEmpty(nuevaFotoBase64))
            {
                if (!string.IsNullOrEmpty(producto.URLFOTO))
                {
                    await _cloudinaryService.DeleteImageAsync(producto.ID_FOTO);
                    producto.ID_FOTO = string.Empty;
                    producto.URLFOTO = string.Empty;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(producto.URLFOTO) && _toolService.EsBase64(nuevaFotoBase64))
                {
                    var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderProductos);
                    producto.ID_FOTO = fotoResponse.PublicId;
                    producto.URLFOTO = fotoResponse.SecureURL;
                }
                else
                {
                    if (nuevaFotoBase64 != producto.URLFOTO && !string.IsNullOrEmpty(producto.URLFOTO))
                    {
                        await _cloudinaryService.DeleteImageAsync(producto.ID_FOTO);

                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderProductos);
                        producto.ID_FOTO = fotoResponse.PublicId;
                        producto.URLFOTO = fotoResponse.SecureURL;
                    }
                    else
                    {
                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderProductos);
                        producto.ID_FOTO = fotoResponse.PublicId;
                        producto.URLFOTO = fotoResponse.SecureURL;
                    }

                }
            }
        }

        private ResponseDTO CreateResponse(string code, bool Success, string message, string value)
        {
            return new ResponseDTO
            {
                Code = code,
                Success = Success,
                Message = message,
                Value = value
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

        private ResponseDTO CreateWarningResponse(string mensaje)
        {
            return new ResponseDTO
            {
                Code = ErrorCodigo.Advertencia,
                Success = Flags.WarningTransaction,
                TitleMessage = TituloResponse.Advertencia,
                Message = mensaje
            };
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

        private ResponseDTO CreateSuccessResponse(int id)
        {
            return new ResponseDTO
            {
                Id = id,
                Success = Flags.SuccessTransaction,
                TitleMessage = GS.SuccessTitleTransaction,
                Message = GS.SuccessTransaction
            };
        }

        private async Task<int> GetIdUsuarioByGuid(Guid idUsuario)
        {
            return await _usuarioRepository.GetIdUsuarioByGuid(idUsuario);
        }

        #endregion

    }
}
