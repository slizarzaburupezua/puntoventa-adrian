using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request
{
    public class IniciaSesionRequest
    {
        [JsonPropertyName("correo")]
        public string Correo { get; set; }

        [JsonPropertyName("clave")]
        public string Clave { get; set; }
    }
}
