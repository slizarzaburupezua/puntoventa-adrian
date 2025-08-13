using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria
{
    public class VentasCategoriasTopDiezDTO
    {
        [JsonPropertyName("colores")]
        [SwaggerSchema("Colores de las categorías")]
        public string[] Color { get; set; }

        [JsonPropertyName("categorias")]
        [SwaggerSchema("Categorias de los productos")]
        public string[] Categorias { get; set; }

        [JsonPropertyName("totalMontos")]
        [SwaggerSchema("Monto total de las categorias")]
        public decimal[] TotalMontos { get; set; }
    }
}
