using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca
{
    public class DistribucionVentasMarcasDTO
    {
        [JsonPropertyName("coloresMarcas")]
        [SwaggerSchema("Lista de las marcas con ventas registradas")]
        public string[] ColoresMarcas { get; set; }

        [JsonPropertyName("nombreMarcas")]
        [SwaggerSchema("Lista de las marcas con ventas registradas")]
        public string[] nombreMarcas { get; set; }

        [JsonPropertyName("totalVentasMarcas")]
        [SwaggerSchema("Lista de las ventas totales de las marcas")]
        public decimal[] TotalVentasMarcas { get; set; }
    }
}
