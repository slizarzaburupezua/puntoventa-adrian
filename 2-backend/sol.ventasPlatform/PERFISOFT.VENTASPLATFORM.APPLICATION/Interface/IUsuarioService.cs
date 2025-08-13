using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> GetPersonalInfoAsync(Guid idUsuarioGuid);

        Task<IList<UsuarioDTO>> GetAllByFilterAsync(ObtenerUsuarioRequest request);

        Task<ResponseDTO> ExistCorreoAsync(ExistCorreoUsuarioRequest request);

        Task<ResponseDTO> ExistNumeroDocumentoAsync(ExistNumeroDocumentoUsuarioRequest request);

        Task<ResponseDTO> InsertAsync(RegistrarUsuarioRequest request);

        Task<ResponseDTO> UpdateAsync(ActualizarUsuarioRequest request);

        Task<ResponseDTO> UpdateActivoAsync(ActualizarActivoUsuarioRequest request);

        Task<ResponseDTO> UpdateUsuarioContraseniaByIdAsync(ActualizarContraseniaRequest request);

        Task<ResponseDTO> DeleteAsync(EliminarUsuarioRequest request);

        Task<ResponseDTO> EnviarEnlacePagoAsync(EnviarEnlacePagoRequest request);

        Task<IList<ObtenerColaboradoresActivosDTO>> GetAllActivesAsync(ObtenerColaboradoresRequest request);
    }
}
