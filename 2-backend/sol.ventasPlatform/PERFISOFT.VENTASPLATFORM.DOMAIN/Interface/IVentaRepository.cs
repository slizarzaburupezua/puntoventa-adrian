using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IVentaRepository
    {
        Task<decimal> SelectTotalVentasAsync(DateTime? fechaInicio, DateTime? fechaFin);

        Task<decimal> SelectTotalVentasAnuladasAsync(DateTime? fechaInicio, DateTime? fechaFin);

        Task<Venta?> SelectByIdAsync(int idVenta);

        Task<IList<ConsultaVentaByFilter>> SelectAllByFilterAsync(FiltroObtenerVenta filtro);

        Task<Venta> InsertAsync(Venta venta);

        Task UpdateAsync(Venta venta);
    }
}
