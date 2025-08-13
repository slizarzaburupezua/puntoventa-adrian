namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta
{
    public class ConsultaDetalleVentaByIdVenta
    {
        public int IdProducto { get; set; }

        public int IdVenta { get; set; }

        public string NumeroVenta { get; set; }

        public int? IdCliente { get; set; }

        public string? NombreCliente { get; set; }

        public string? Direccion { get; set; }

        public string? UrlBoletaFactura { get; set; }

        public DateTime FechaVenta { get; set; }    
         
        public string? UrlFotoProducto { get; set; }

        public string NombreProducto { get; set; }

        public string? ColorProducto { get; set; }

        public string NombreCategoria { get; set; }

        public string? ColorCategoria { get; set; }

        public string NombreMarca { get; set; }

        public string? ColorMarca { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioProducto { get; set; }

        public decimal PrecioTotal { get; set; }

        public string? NotaAdicional { get; set; }
    }
}
