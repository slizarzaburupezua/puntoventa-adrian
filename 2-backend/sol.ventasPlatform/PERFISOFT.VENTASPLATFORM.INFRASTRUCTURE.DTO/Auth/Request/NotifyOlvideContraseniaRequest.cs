using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request
{
    public class NotifyOlvideContraseniaRequest
    {
        [JsonPropertyName("correo")]
        [SwaggerSchema("Correo del usuario.")]
        public string Correo { get; set; }

        [JsonPropertyName("destinationTimeZone")]
        [SwaggerSchema("Zona horaria del usuario.")]
        public string DestinationTimeZone { get; set; }
    }
}
