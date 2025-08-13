using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Categorias
{
    public interface ICategoriasInfrastructureService
    {
        Task<byte[]> GenerarReportePorCategoriasAsync(FiltroReportePorCategorias filtro);
    }
}
