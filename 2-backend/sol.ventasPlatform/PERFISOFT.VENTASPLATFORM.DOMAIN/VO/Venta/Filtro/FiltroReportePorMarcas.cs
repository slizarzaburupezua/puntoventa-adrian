using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro
{
    public class FiltroReportePorMarcas
    {
        public List<DetalleVenta> LstDetalleVenta { get; set; }

        public List<ConsultaTotalMarcas> LstDetalleTotalMarcas { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public string CodigoMoneda { get; set; }
    }
}
