using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IVentaNumeroCorrelativoRepository
    {
        Task<VentaNumeroCorrelativo> SelectNextCorrelativoAsync(string serie);

        Task UpdateCorrelativoAsync(string serie);
    }
}
