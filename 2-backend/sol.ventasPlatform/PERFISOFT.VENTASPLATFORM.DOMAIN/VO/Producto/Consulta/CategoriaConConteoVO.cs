namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Consulta
{
    public class CategoriaConConteoVO
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int CantidadProductos { get; set; }
    }
}
