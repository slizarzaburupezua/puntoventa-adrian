namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Filtro
{
    public class FiltroConsultaProducto
    {
        public int[] LST_CATEGORIAS { get; set; }

        public int[] LST_MARCAS { get; set; }

        public string? CODIGO { get; set; }

        public string? NOMBRE { get; set; }

        public DateTime? FECHA_REGISTRO_INICIO { get; set; }

        public DateTime? FECHA_REGISTRO_FIN { get; set; }

        public decimal? PRECIO_COMPRA_INICIO { get; set; }

        public decimal? PRECIO_COMPRA_FIN { get; set; }

        public decimal? PRECIO_VENTA_INICIO { get; set; }

        public decimal? PRECIO_VENTA_FIN { get; set; }

    }
}
