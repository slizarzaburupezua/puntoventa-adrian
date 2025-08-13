using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca
{
    public class VentasMarcasTopDiezDTO
    {
        [JsonPropertyName("colores")]
        [SwaggerSchema("Colores de las marcas")]
        public string[] Color { get; set; }

        [JsonPropertyName("marcas")]
        [SwaggerSchema("Marcas de los productos")]
        public string[] Marcas { get; set; }

        [JsonPropertyName("totalMontos")]
        [SwaggerSchema("Monto total de las marcas")]
        public decimal[] TotalMontos { get; set; }
    }
}
