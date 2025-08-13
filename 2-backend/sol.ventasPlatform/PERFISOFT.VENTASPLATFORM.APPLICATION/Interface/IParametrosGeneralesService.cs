using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IParametrosGeneralesService
    {
        Task<List<RolDTO>> GetAllRolAsync();

        Task<List<MonedaDTO>> GetAllMonedaAsync();

        Task<List<TipoDocumentoDTO>> GetAllTipoDocumentoAsync();

        Task<List<GeneroDTO>> GetAllGeneroAsync();

        Task<NegocioDTO> GetNegocioAsync();

        Task<ResponseDTO> UpdateNegocioAsync(ActualizarNegocioRequest request);

        Task<ResponseDTO> VistaPreviaBoletaFacturaAsync(ActualizarNegocioRequest request);
    }
}
