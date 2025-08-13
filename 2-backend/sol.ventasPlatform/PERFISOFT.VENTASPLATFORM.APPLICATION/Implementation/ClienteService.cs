using AutoMapper;
using Microsoft.AspNetCore.Http;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Cliente.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Response;
using Serilog;
using GE = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using GS = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class ClienteService : IClienteService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IToolService _toolService;

        public ClienteService(IMapper mapper,
                              IClienteRepository clienteRepository,
                              IUsuarioRepository usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IToolService toolService

            )
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
            _toolService = toolService;
        }

        public async Task<IList<ClienteDTO>> GetAllByFilterAsync(ObtenerClienteRequest request)
        {
            return _mapper.Map<List<ClienteDTO>>(await _clienteRepository.SelectAllByFilterAsync(_mapper.Map<FiltroConsultaCliente>(request)));
        }

        public async Task<ResponseDTO> ExistCorreoAsync(ExistCorreoClienteRequest request)
        {
            Log.Information(LogMessages.Cliente.ExistCorreoAsync.Initial, request.Correo);

            if (await _clienteRepository.ExistCorreoAsync(request.Correo))
            {
                Log.Warning(LogMessages.Cliente.ExistCorreoAsync.ClienteExiste, request.Correo);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.Existe, string.Format(LogMessages.Cliente.ExistCorreoAsync.ClienteExiste, request.Correo), string.Empty);
            }

            return _toolService.CreateResponse(string.Empty, Flags.NoExiste, string.Empty, string.Empty);
        }

        public async Task<ResponseDTO> ExistNumeroDocumentoAsync(ExistNumeroDocumentoClienteRequest request)
        {
            Log.Information(LogMessages.Cliente.ExistNumeroDocumentoAsync.Initial, request.NumeroDocumento);

            if (await _clienteRepository.ExistNumeroDocumentoAsync(request.NumeroDocumento))
            {
                Log.Warning(LogMessages.Cliente.ExistNumeroDocumentoAsync.ClienteExiste, request.NumeroDocumento);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.Existe, string.Format(LogMessages.Cliente.ExistNumeroDocumentoAsync.ClienteExiste, request.NumeroDocumento), string.Empty);
            }

            return _toolService.CreateResponse(string.Empty, Flags.NoExiste, string.Empty, string.Empty);
        }

        public async Task<ClienteDTO> GetByNumDocumentoCorreoAsync(ObtenerClientePorNumDocumentoCorreoRequest request)
        {
            return _mapper.Map<ClienteDTO>(await _clienteRepository.SelectByNumDocumentoCorreoAsync(request.Parametro));
        }

        public async Task<ResponseDTO> InsertAsync(RegistrarClienteRequest request)
        {
            Log.Information(LogMessages.Cliente.InsertAsync.Initial);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Cliente.InsertAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            if (await _clienteRepository.ExistCorreoAsync(request.CorreoElectronico.Trim()))
            {
                Log.Warning(LogMessages.Cliente.ExistCorreoAsync.ClienteExiste, request.CorreoElectronico);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.WarningTransaction, string.Format(LogMessages.Cliente.ExistCorreoAsync.ClienteExiste, request.CorreoElectronico), string.Empty);
            }

            if (await _clienteRepository.ExistNumeroDocumentoAsync(request.NumeroDocumento.Trim()))
            {
                Log.Warning(LogMessages.Cliente.ExistNumeroDocumentoAsync.ClienteExiste, request.NumeroDocumento);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.WarningTransaction, string.Format(LogMessages.Cliente.ExistNumeroDocumentoAsync.ClienteExiste, request.NumeroDocumento), string.Empty);
            }

            var response = _mapper.Map<ResponseDTO>(await _clienteRepository.InsertAsync(_mapper.Map<Cliente>(request)));

            Log.Information(LogMessages.Cliente.InsertAsync.Finish, request.IdUsuarioGuid);

            return response;
        }

        public async Task<ResponseDTO> UpdateAsync(ActualizarClienteRequest request)
        {
            Log.Information(LogMessages.Cliente.UpdateAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Cliente.UpdateAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var clienteEntity = await _clienteRepository.SelectByIdAsync(request.Id);

            if (clienteEntity is null)
            {
                Log.Error(LogMessages.Cliente.UpdateAsync.ClienteNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            clienteEntity.NOMBRES = request.Nombres.Trim() ?? string.Empty;
            clienteEntity.APELLIDOS = request.Apellidos.Trim() ?? string.Empty;
            clienteEntity.CELULAR = request.Celular.Trim() ?? string.Empty;
            clienteEntity.DIRECCION = request.Direccion.Trim() ?? string.Empty;
            clienteEntity.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);

            var updateCliente = await _clienteRepository.UpdateAsync(clienteEntity);

            Log.Information(LogMessages.Cliente.UpdateAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(updateCliente.ID);
        }

        public async Task<ResponseDTO> DeleteAsync(EliminarClienteRequest request)
        {
            Log.Information(LogMessages.Cliente.DeleteAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Cliente.DeleteAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var clienteEntity = await _clienteRepository.SelectByIdAsync(request.Id);

            if (clienteEntity is null)
            {
                Log.Error(LogMessages.Cliente.DeleteAsync.ClienteNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            clienteEntity.ESTADO = Flags.Deshabilitar;
            clienteEntity.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            clienteEntity.MOTIVO_ANULACION = request.MotivoAnulacion ?? string.Empty;

            var modifyGastoCategoriaEntity = await _clienteRepository.UpdateAsync(clienteEntity);

            Log.Information(LogMessages.Cliente.DeleteAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(modifyGastoCategoriaEntity.ID);
        }

        public async Task<ResponseDTO> UpdateActivoAsync(ActualizarActivoClienteRequest request)
        {
            Log.Information(LogMessages.Cliente.UpdateActivoAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Cliente.UpdateActivoAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var clienteEntity = await _clienteRepository.SelectByIdAsync(request.Id);

            if (clienteEntity is null)
            {
                Log.Error(LogMessages.Cliente.UpdateActivoAsync.ClienteNoExiste, request.Id);
                return CreateErrorEntityResponse(request.Id);
            }

            await _clienteRepository.UpdateActivoAsync(clienteEntity.ID, request.Activo);

            Log.Information(LogMessages.Cliente.UpdateActivoAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(clienteEntity.ID);
        }

        #region PRIVATE METHODS

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
