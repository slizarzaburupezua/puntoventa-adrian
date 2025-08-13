using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class EnviarEnlacePagoRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("correo")]
        public string Correo { get; set; }
    }
}
