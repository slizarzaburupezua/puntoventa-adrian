using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class ActualizarContraseniaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("contraseniaActual")]
        [SwaggerSchema("Contrasenia Actual")]
        public string ContraseniaActual { get; set; }

        [JsonPropertyName("contraseniaNueva")]
        [SwaggerSchema("Contrasenia Nueva")]
        public string ContraseniaNueva { get; set; }
    }
}
