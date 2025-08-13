namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro
{
    public class FiltroDetalleVentaProductoReporte
    {
        public int[] LstProductos { get; set; }

        public DateTime? FechaVentaInicio { get; set; }

        public DateTime? FechaVentaFin { get; set; }
    }
}
