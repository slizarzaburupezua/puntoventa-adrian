using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Marcas
{
    public interface IMarcasInfrastructureService
    {
        Task<byte[]> GenerarReportePorMarcasAsync(FiltroReportePorMarcas filtro);
    }
}
