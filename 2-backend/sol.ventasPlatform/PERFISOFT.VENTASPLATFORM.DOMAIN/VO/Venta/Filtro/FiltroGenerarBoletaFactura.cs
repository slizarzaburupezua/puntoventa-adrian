using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro
{
    public class FiltroGenerarBoletaFactura
    {
        public Negocio InformacionNegocio { get; set; }

        public List<DetalleVenta> LstDetalleVenta { get; set; }

        public Entities.Cliente InformacionCliente { get; set; }

        public Entities.Venta Venta { get; set; }

    }
}
