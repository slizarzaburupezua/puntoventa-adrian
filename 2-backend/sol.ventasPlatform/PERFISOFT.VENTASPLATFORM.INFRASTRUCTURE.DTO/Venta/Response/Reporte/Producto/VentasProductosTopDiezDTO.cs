using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto
{
    public class VentasProductosTopDiezDTO
    {
        [JsonPropertyName("colores")]
        [SwaggerSchema("Colores de las productos")]
        public string[] Color { get; set; }

        [JsonPropertyName("productos")]
        [SwaggerSchema("Lista de Productos")]
        public string[] Productos { get; set; }

        [JsonPropertyName("totalMontos")]
        [SwaggerSchema("Monto total de los productos")]
        public decimal[] TotalMontos { get; set; }
    }
}
