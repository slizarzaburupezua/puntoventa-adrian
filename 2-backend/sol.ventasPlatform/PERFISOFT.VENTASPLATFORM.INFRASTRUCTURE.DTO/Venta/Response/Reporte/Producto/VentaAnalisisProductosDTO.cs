using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto
{
    public class VentaAnalisisProductosDTO
    {
        [JsonPropertyName("lstEvolucionVentasProductoFecha")]
        [SwaggerSchema("Evolución de las ventas por productos y fechas")]
        public List<EvolucionVentasProductosFechaDTO> LstEvolucionVentasProductoFecha { get; set; }

        [JsonPropertyName("evolucionVentasFecha")]
        [SwaggerSchema("Evolucion de las ventas por fechas")]
        public EvolucionVentasFechaDTO EvolucionVentasFecha { get; set; }

        [JsonPropertyName("distribucionVentasProducto")]
        [SwaggerSchema("Montos de los Productos de ventas con su porcentaje")]
        public DistribucionVentasProductosDTO DistribucionVentasProducto { get; set; }

        [JsonPropertyName("topDiezProductosVentas")]
        [SwaggerSchema("Los 10 Productos con más ventas segun la fecha filtrada")]
        public VentasProductosTopDiezDTO TopDiezProductosVentas { get; set; }
    }
}
