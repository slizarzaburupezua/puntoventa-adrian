namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro
{
    public class FiltroRegistrarVenta
    {
        public int IdUsuario { get; set; }

        public int? IdCliente { get; set; }

        public string NumeroVenta { get; set; }

        public decimal PrecioTotal { get; set; }

        public DateTime FechaVenta { get; set; }

        public string NotaAdicional { get; set; }

        public bool Estado { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
