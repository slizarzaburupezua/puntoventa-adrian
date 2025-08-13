using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Productos
{
    public interface IProductosInfrastructureService
    {
        Task<byte[]> GenerarReportePorProductosAsync(FiltroReportePorProductos filtro);
    }
}
