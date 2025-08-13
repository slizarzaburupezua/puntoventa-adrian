using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte
{
    public class EvolucionVentasFechaDTO
    {
        [JsonPropertyName("fechaVenta")]
        [SwaggerSchema("Fecha de la venta")]
        public DateTime[] FechaVenta { get; set; }

        [JsonPropertyName("totalVenta")]
        [SwaggerSchema("Monto total de la venta")]
        public decimal[] TotalVenta { get; set; }
    }
}
