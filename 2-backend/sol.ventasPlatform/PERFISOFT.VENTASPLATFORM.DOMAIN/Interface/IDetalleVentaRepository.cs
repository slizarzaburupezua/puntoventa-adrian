using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IDetalleVentaRepository
    {
        Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroDetalleVentaProductoReporte filtro);

        Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroDetalleVentaCategoriaReporte filtro);

        Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroDetalleVentaMarcaReporte filtro);

        Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroResumenReporte filtro);

        Task<List<ConsultaTotalCategorias>> SelectCategoriasTotalsByDateAsync(DateTime? fechaVentaInicio, DateTime? fechaVentaFin, int[] lstCategorias);

        Task<List<ConsultaTotalMarcas>> SelectMarcasTotalsByDateAsync(DateTime? fechaVentaInicio, DateTime? fechaVentaFin, int[] lstMarcas);

        Task<List<ConsultaTotalProductos>> SelectProductosTotalsByDateAsync(DateTime? fechaVentaInicio, DateTime? fechaVentaFin, int[] lstProductos);

        Task<IList<ConsultaDetalleVentaByIdVenta>> SelectDetalleAsync(int? idVenta, string[]? LstUsuario, string? numeroVenta, DateTime? fechaVentaInicio, DateTime? fechaVentaFin);

        Task<IList<ConsultaDetalleVentaByIdVenta>> SelectDetalleAsync(int idVenta);

        Task<List<DetalleVenta>> InsertAsync(List<DetalleVenta> detalleVenta);
    }
}
