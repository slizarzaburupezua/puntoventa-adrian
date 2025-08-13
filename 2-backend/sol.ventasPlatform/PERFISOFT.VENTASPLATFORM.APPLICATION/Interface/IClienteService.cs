using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IClienteService
    {
        Task<IList<ClienteDTO>> GetAllByFilterAsync(ObtenerClienteRequest request);

        Task<ResponseDTO> ExistCorreoAsync(ExistCorreoClienteRequest request);

        Task<ResponseDTO> ExistNumeroDocumentoAsync(ExistNumeroDocumentoClienteRequest request);

        Task<ClienteDTO> GetByNumDocumentoCorreoAsync(ObtenerClientePorNumDocumentoCorreoRequest request);

        Task<ResponseDTO> InsertAsync(RegistrarClienteRequest request);

        Task<ResponseDTO> UpdateAsync(ActualizarClienteRequest request);

        Task<ResponseDTO> UpdateActivoAsync(ActualizarActivoClienteRequest request);

        Task<ResponseDTO> DeleteAsync(EliminarClienteRequest request);
    }
}
