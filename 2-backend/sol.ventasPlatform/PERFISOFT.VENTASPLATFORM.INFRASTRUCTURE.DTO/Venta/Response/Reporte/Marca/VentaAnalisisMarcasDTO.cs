using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca
{
    public class VentaAnalisisMarcasDTO
    {
        [JsonPropertyName("lstEvolucionVentasMarcaFecha")]
        [SwaggerSchema("Evolucin de las ventas por marcas y fechas")]
        public List<EvolucionVentasMarcaFechaDTO> LstEvolucionVentasMarcaFecha { get; set; }

        [JsonPropertyName("evolucionVentasFecha")]
        [SwaggerSchema("Evolucion de las ventas por fechas")]
        public EvolucionVentasFechaDTO EvolucionVentasFecha { get; set; }

        [JsonPropertyName("distribucionVentasMarca")]
        [SwaggerSchema("Montos de la Marcas de ventas con su porcentaje")]
        public DistribucionVentasMarcasDTO DistribucionVentasMarca { get; set; }

        [JsonPropertyName("topDiezMarcasVentas")]
        [SwaggerSchema("Las 10 marcas con más ventas segun la fecha filtrada")]
        public VentasMarcasTopDiezDTO TopDiezMarcasVentas { get; set; }
    }
}
