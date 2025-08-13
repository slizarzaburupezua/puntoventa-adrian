using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Parametro.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IParametroService
    {
        Task<ResponseDTO> GetValueBySubKeyAsync(string key, string subKey);

        Task<IList<ParametroDTO>> GetAllAsync();

        Task<IList<ParametroDetalleDTO>> GetAllDetalleByIdAsync(int idParametro, Guid idUsuarioGuid);

        Task<ResponseDTO> UpdateDetalleParametroAsync(ActualizarParametroDetalleRequest request);

        Task<IList<ParametroDetalleDTO>> GetAllDetalleByKeyAsync(string keyParam);
    }
}
