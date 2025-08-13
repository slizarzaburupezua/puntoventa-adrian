using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.DetalleVenta.Request
{
    public class ObtenerReportePorProductosRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        [SwaggerSchema("Ubicación del usuario.")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("lstProductos")]
        [SwaggerSchema("Lista de productos")]
        public int[] LstProductos { get; set; }

        [JsonPropertyName("fechaVentaInicio")]
        [SwaggerSchema("Rango de Feha venta inicio")]
        public DateTime? FechaVentaInicio { get; set; }

        [JsonPropertyName("fechaVentaFin")]
        [SwaggerSchema("Rango de Feha venta fin")]
        public DateTime? FechaVentaFin { get; set; }
    }
}
