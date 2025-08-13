using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class ActualizarActivoUsuarioRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario que realiza la actualización.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("idUsuarioSeleccionado")]
        [SwaggerSchema("Identificador del usuario a actualizar")]
        public Guid IdUsuarioSeleccionado { get; set; }

        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si se va a activar o desactivar.")]
        public bool Activo { get; set; }
    }
}
