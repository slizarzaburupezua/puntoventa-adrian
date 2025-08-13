using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria
{
    public class DistribucionVentasCategoriaDTO
    {
        [JsonPropertyName("coloresCategorias")]
        [SwaggerSchema("Lista de los colores de las categorías con gasto registrado")]
        public string[] ColoresCategorias { get; set; }

        [JsonPropertyName("nombreCategorias")]
        [SwaggerSchema("Lista de las categorías con ventas registrado")]
        public string[] NombreCategorias { get; set; }

        [JsonPropertyName("totalVentasCategorias")]
        [SwaggerSchema("Lista de las ventas totales de las categorias")]
        public decimal[] TotalVentasCategorias { get; set; }
    }
}
