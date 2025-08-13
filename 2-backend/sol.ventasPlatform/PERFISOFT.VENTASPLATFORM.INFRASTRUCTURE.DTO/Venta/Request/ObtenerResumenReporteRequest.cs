using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request
{
    public class ObtenerResumenReporteRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        [SwaggerSchema("Ubicación del usuario.")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario que está consultando.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("fechaInicio")]
        [SwaggerSchema("Feha inicio del filtro")]
        public DateTime FechaInicio { get; set; }

        [JsonPropertyName("fechaFin")]
        [SwaggerSchema("Feha fin del filtro")]
        public DateTime FechaFin { get; set; }

    }
}
