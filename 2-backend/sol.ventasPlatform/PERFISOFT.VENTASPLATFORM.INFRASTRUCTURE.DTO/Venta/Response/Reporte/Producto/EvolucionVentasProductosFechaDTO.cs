using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto
{
    public class EvolucionVentasProductosFechaDTO
    {
        [JsonPropertyName("nombreProducto")]
        [SwaggerSchema("Nombre del Producto")]
        public string NombreProducto { get; set; }

        [JsonPropertyName("colorProducto")]
        [SwaggerSchema("Color del Producto")]
        public string ColorProducto { get; set; }

        [JsonPropertyName("datosVentasAgrupados")]
        [SwaggerSchema("Datos de la venta")]
        public List<VentasAnalisisAgrupadosProductosDTO> DatosVentasAgrupados { get; set; }
    }
}
