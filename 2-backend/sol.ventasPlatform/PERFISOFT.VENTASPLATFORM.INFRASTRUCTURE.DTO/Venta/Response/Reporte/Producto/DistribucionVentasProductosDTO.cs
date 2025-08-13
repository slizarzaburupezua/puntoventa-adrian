using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto
{
    public class DistribucionVentasProductosDTO
    {
        [JsonPropertyName("coloresProductos")]
        [SwaggerSchema("Lista de los productos con ventas registradas")]
        public string[] ColoresProductos { get; set; }

        [JsonPropertyName("nombreProductos")]
        [SwaggerSchema("Lista de los productos con ventas registradas")]
        public string[] NombreProductos { get; set; }

        [JsonPropertyName("totalVentasProductos")]
        [SwaggerSchema("Lista de los productos totales")]
        public decimal[] TotalVentasProductos { get; set; }
    }
}
