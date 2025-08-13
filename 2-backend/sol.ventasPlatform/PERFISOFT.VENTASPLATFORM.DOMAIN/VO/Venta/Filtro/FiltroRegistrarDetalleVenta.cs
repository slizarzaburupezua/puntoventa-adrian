namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro
{
    public class FiltroRegistrarDetalleVenta
    {
        public string DestinationTimeZoneIdRegistro { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        public string UrlFotoProducto { get; set; }

        public string NombreProducto { get; set; }

        public string ColorProducto { get; set; }

        public string NombreCategoria { get; set; }

        public string ColorCategoria { get; set; }

        public string NombreMarca { get; set; }

        public string ColorMarca { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal PrecioTotal { get; set; }

        public bool Activo { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
