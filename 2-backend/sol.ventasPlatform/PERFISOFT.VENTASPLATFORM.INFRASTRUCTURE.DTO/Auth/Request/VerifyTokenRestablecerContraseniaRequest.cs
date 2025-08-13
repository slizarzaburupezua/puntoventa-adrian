using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request
{
    public class VerifyTokenRestablecerContraseniaRequest
    {
        [JsonPropertyName("token")]
        [SwaggerSchema("Token OTP.")]
        public string Token { get; set; }

        [JsonPropertyName("destinationTimeZone")]
        [SwaggerSchema("Zona horaria del usuario.")]
        public string DestinationTimeZone { get; set; }
    }
}
