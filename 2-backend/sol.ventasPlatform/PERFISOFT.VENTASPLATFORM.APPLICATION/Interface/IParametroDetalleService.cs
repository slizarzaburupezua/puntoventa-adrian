using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IParametroDetalleService
    {
        Task<IList<ParametroDetalleDTO>> GetAllAsync();
    }
}
