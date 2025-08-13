using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class EliminarUsuarioRequest
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

        [JsonPropertyName("motivoAnulacion")]
        [SwaggerSchema("Motivo anulación del cliente")]
        public string MotivoAnulacion { get; set; }

    }
}
