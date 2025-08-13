using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class ActualizarUsuarioRequest
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

        [JsonPropertyName("idRol")]
        [SwaggerSchema("Rol")]
        public int IdRol { get; set; }

        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del cliente.")]
        public string Celular { get; set; }

        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección del cliente.")]
        public string Direccion { get; set; }

        [JsonPropertyName("nombreArchivo")]
        [SwaggerSchema("Nombre del archivo de la foto.")]
        public string? NombreArchivo { get; set; }

        [JsonPropertyName("foto")]
        [SwaggerSchema("Foto del usuario.")]
        public string? Foto { get; set; }

    }
}
