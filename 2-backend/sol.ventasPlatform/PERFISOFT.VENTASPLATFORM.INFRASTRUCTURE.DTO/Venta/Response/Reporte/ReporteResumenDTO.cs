using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte
{
    public class ReporteResumenDTO
    {
        [JsonPropertyName("totalClientesRegistrados")]
        [SwaggerSchema("Total clientes registrados")]
        public int TotalClientesRegistrados { get; set; }

        [JsonPropertyName("totalProductosRegistrados")]
        [SwaggerSchema("Total productos registrados")]
        public int TotalProductosRegistrados { get; set; }

        [JsonPropertyName("totalVentasRegistradas")]
        [SwaggerSchema("Total ventas registradas")]
        public decimal TotalVentasRegistradas { get; set; }

        [JsonPropertyName("totalVentasAnuladas")]
        [SwaggerSchema("Total ventas anuladas")]
        public decimal TotalVentasAnuladas { get; set; }

        [JsonPropertyName("evolucionVentasFecha")]
        [SwaggerSchema("Evolucion de las ventas por fechas")]
        public EvolucionVentasFechaDTO EvolucionVentasFecha { get; set; }

        [JsonPropertyName("distribucionVentasMarca")]
        [SwaggerSchema("Montos de la Marcas de ventas con su porcentaje")]
        public DistribucionVentasMarcasDTO DistribucionVentasMarca { get; set; }

        [JsonPropertyName("distribucionVentasProducto")]
        [SwaggerSchema("Montos de los Productos de ventas con su porcentaje")]
        public DistribucionVentasProductosDTO DistribucionVentasProductos { get; set; }

        [JsonPropertyName("topDiezMarcasVentas")]
        [SwaggerSchema("Las 10 marcas con más ventas segun la fecha filtrada")]
        public VentasMarcasTopDiezDTO TopDiezMarcasVentas { get; set; }

        [JsonPropertyName("topDiezProductosVentas")]
        [SwaggerSchema("Los 10 productos con más ventas segun la fecha filtrada")]
        public VentasProductosTopDiezDTO TopDiezProductosVentas { get; set; }
    }
}
