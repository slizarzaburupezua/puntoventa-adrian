using AutoMapper;
using Microsoft.AspNetCore.Http;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Parametro.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService;
using Serilog;
using GE = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using GS = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class ParametroService : IParametroService
    {
        private readonly IMapper _mapper;
        private readonly IParametroRepository _parametroRepository;
        private readonly IParametroDetalleRepository _parametroDetalleRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IToolService _toolService;

        public ParametroService(IMapper mapper,
                              IParametroRepository parametroRepository,
                              IParametroDetalleRepository parametroDetalleRepository,
                              IUsuarioRepository usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IToolService toolService,
            ICloudinaryService cloudinaryService
            )
        {
            _mapper = mapper;
            _parametroRepository = parametroRepository;
            _parametroDetalleRepository = parametroDetalleRepository;
            _usuarioRepository = usuarioRepository;
            _toolService = toolService;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ResponseDTO> GetValueBySubKeyAsync(string key, string subKey)
        {
            var valueKey = await _parametroDetalleRepository.SelectValueBySubKeyAsync(key, subKey);
            return new ResponseDTO() { Success = Flags.SuccessTransaction, Value = valueKey };
        }

        public async Task<IList<ParametroDTO>> GetAllAsync()
        {
            return _mapper.Map<List<ParametroDTO>>(await _parametroRepository.SelectAllAsync());
        }

        public async Task<IList<ParametroDetalleDTO>> GetAllDetalleByIdAsync(int idParametro, Guid idUsuarioGuid)
        {
            var idUsuario = await GetIdUsuarioByGuid(idUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
                return new List<ParametroDetalleDTO>();

            return _mapper.Map<List<ParametroDetalleDTO>>(await _parametroDetalleRepository.SelectAllByIdAsync(idParametro));
        }

        public async Task<IList<ParametroDetalleDTO>> GetAllDetalleByKeyAsync(string keyParam)
        {
            return _mapper.Map<List<ParametroDetalleDTO>>(await _parametroDetalleRepository.SelectAllByKeyAsync(keyParam));
        }

        public async Task<ResponseDTO> UpdateDetalleParametroAsync(ActualizarParametroDetalleRequest request)
        {
            Log.Information(LogMessages.Parametro.UpdateDetalleParametroAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Parametro.UpdateDetalleParametroAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var detalleParamatroEntity = await _parametroDetalleRepository.SelecByIdAsync(request.Id);

            if (detalleParamatroEntity is null)
            {
                Log.Error(LogMessages.Parametro.UpdateDetalleParametroAsync.DetalleParametroNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            if (request.ParaKey == Parametros.ParaKey.LOGO_STM && request.TipoCampo == Parametros.TipoCampo.IMAGEN)
            {
                if (request.Svalor2 != detalleParamatroEntity.SVALOR2)
                    await GestionarFoto(detalleParamatroEntity, request.Svalor1, request.Svalor2);
            }

            if (request.ParaKey == Parametros.ParaKey.LOGO_STM && request.TipoCampo == Parametros.TipoCampo.URL)
            {
                detalleParamatroEntity.SVALOR1 = null;
                detalleParamatroEntity.SVALOR2 = string.IsNullOrWhiteSpace(request.Svalor2) ? null : request.Svalor2;
            }

            detalleParamatroEntity.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            detalleParamatroEntity.TIPOCAMPO = request.TipoCampo;

            var updateParametroDetalle = await _parametroDetalleRepository.UpdateAsync(detalleParamatroEntity);

            Log.Information(LogMessages.Parametro.UpdateDetalleParametroAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(updateParametroDetalle.ID);
        }

        private async Task GestionarFoto(ParametroDetalle parametroDetalle, string nombreArchivoFoto, string nuevaFotoBase64)
        {
            if (string.IsNullOrEmpty(nuevaFotoBase64))
            {
                if (!string.IsNullOrEmpty(parametroDetalle.SVALOR2))
                {
                    if (!string.IsNullOrEmpty(parametroDetalle.SVALOR1))
                        await _cloudinaryService.DeleteImageAsync(parametroDetalle.SVALOR1);

                    parametroDetalle.SVALOR1 = null;
                    parametroDetalle.SVALOR2 = null;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(parametroDetalle.SVALOR2) && _toolService.EsBase64(nuevaFotoBase64))
                {
                    var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderParametros);
                    parametroDetalle.SVALOR1 = fotoResponse.PublicId;
                    parametroDetalle.SVALOR2 = fotoResponse.SecureURL;
                }
                else
                {
                    if (!string.IsNullOrEmpty(parametroDetalle.SVALOR1) && !string.IsNullOrEmpty(parametroDetalle.SVALOR2))
                    {
                        await _cloudinaryService.DeleteImageAsync(parametroDetalle.SVALOR1);

                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderParametros);
                        parametroDetalle.SVALOR1 = fotoResponse.PublicId;
                        parametroDetalle.SVALOR2 = fotoResponse.SecureURL;
                    }
                    else
                    {
                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderParametros);
                        parametroDetalle.SVALOR1 = fotoResponse.PublicId;
                        parametroDetalle.SVALOR2 = fotoResponse.SecureURL;
                    }
                }
            }
        }

        private async Task<int> GetIdUsuarioByGuid(Guid idUsuario)
        {
            return await _usuarioRepository.GetIdUsuarioByGuid(idUsuario);
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
    }
}
