namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta
{
    public class ConsultaVentaByFilter
    {
        public int IdVenta { get; set; }

        public string NumeroVenta { get; set; }

        public string? NombreCliente { get; set; }

        public string? CorreoUsuario { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal TotalVenta { get; set; }

        public string UrlBoletaFactura { get; set; }

        public bool Estado { get; set; }
    }
}
