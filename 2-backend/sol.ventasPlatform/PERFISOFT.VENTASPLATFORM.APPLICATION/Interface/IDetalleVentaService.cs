using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IDetalleVentaService
    {
        Task<IList<DetalleVentaDTO>> GetDetalleAsync(ObtenerDetalleVentaRequest request);

        Task<VentaAnalisisProductosDTO> GetAnalisisProductosByFilterAsync(ObtenerReporteProductoRequest request);

        Task<VentaAnalisisCategoriasDTO> GetAnalisisCategoriasByFilterAsync(ObtenerReporteCategoriaRequest request);

        Task<VentaAnalisisMarcasDTO> GetAnalisisMarcasByFilterAsync(ObtenerReporteMarcaRequest request);

        Task<ReporteResumenDTO> GetResumenReporteAsync(ObtenerResumenReporteRequest request);

        Task<ResponseDTO> GetReportePorCategoriasAsync(ObtenerReporteCategoriaRequest request);

        Task<ResponseDTO> GetReportePorMarcasAsync(ObtenerReporteMarcaRequest request);

        Task<ResponseDTO> GetReportePorProductosAsync(ObtenerReporteProductoRequest request);
    }
}
