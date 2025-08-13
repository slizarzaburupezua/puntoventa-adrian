using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request
{
    public class ObtenerDetalleVentaRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("idVenta")]
        [SwaggerSchema("Identificador de la venta.")]
        public int? IdVenta { get; set; }

        [JsonPropertyName("lstUsuario")]
        [SwaggerSchema("Lista de usuarios que realizaron las ventas")]
        public string[]? LstUsuario { get; set; }

        [JsonPropertyName("numeroVenta")]
        [SwaggerSchema("Número de venta")]
        public string? NumeroVenta { get; set; }

        [JsonPropertyName("fechaVentaInicio")]
        [SwaggerSchema("Feha inicio de la venta")]
        public DateTime? FechaVentaInicio { get; set; }

        [JsonPropertyName("fechaVentaFin")]
        [SwaggerSchema("Feha fin de la venta")]
        public DateTime? FechaVentaFin { get; set; }

    }
}
