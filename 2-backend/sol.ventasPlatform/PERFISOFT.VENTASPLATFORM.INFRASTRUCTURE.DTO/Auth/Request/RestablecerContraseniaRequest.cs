using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request
{
    public class RestablecerContraseniaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("token")]
        [SwaggerSchema("Token que se le envia al usuario a su correo")]
        public string Token { get; set; }

        [JsonPropertyName("contrasenia")]
        [SwaggerSchema("Contrasenia Nueva")]
        public string Contrasenia { get; set; }
    }
}
