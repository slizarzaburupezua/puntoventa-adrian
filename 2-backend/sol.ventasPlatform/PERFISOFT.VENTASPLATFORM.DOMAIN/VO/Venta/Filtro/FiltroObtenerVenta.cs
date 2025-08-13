namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro
{
    public class FiltroObtenerVenta
    {
        public string[] LstUsuario { get; set; }

        public string NumeroVenta { get; set; }

        public DateTime? FechaVentaInicio { get; set; }

        public DateTime? FechaVentaFin { get; set; }

        public decimal? MontoVentaInicio { get; set; }

        public decimal? MontoVentaFin { get; set; }
    }
}
