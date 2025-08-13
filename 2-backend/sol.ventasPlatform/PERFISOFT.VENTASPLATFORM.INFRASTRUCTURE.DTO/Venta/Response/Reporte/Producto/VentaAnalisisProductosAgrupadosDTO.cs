using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto
{
    public class VentaAnalisisProductosAgrupadosDTO
    {
        [JsonPropertyName("nombreProducto")]
        [SwaggerSchema("Nombre del producto")]
        public string NombreProducto { get; set; }

        [JsonPropertyName("colorProducto")]
        [SwaggerSchema("Color del Producto")]
        public string ColorProducto { get; set; }

        [JsonPropertyName("fechaVenta")]
        [SwaggerSchema("Fecha de la venta")]
        public DateTime FechaVentaAgrupada { get; set; }

        [JsonPropertyName("montoTotal")]
        [SwaggerSchema("Suma total de las ventas agrupados por el Producto")]
        public decimal MontoTotal { get; set; }

        [JsonPropertyName("datosVentas")]
        [SwaggerSchema("Datos de la venta")]
        public List<VentasAnalisisAgrupadosProductosDTO> Datos { get; set; }
    }
}
