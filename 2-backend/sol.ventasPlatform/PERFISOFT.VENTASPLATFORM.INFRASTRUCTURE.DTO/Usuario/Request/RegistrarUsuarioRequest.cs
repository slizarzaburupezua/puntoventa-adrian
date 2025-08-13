using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class RegistrarUsuarioRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("destinationTimeZoneIdRegistro")]
        [SwaggerSchema("Localización del registro")]
        public string DestinationTimeZoneIdRegistro { get; set; }

        [JsonPropertyName("idRol")]
        [SwaggerSchema("Rol")]
        public int IdRol { get; set; }

        [JsonPropertyName("idTipoDocumento")]
        [SwaggerSchema("Identificador del tipo del documento del usuario")]
        public int IdTipoDocumento { get; set; }

        [JsonPropertyName("numeroDocumento")]
        [SwaggerSchema("Número de documento del usuario")]
        public string NumeroDocumento { get; set; }

        [JsonPropertyName("nombres")]
        [SwaggerSchema("Nombres completos")]
        public string Nombres { get; set; }

        [JsonPropertyName("apellidos")]
        [SwaggerSchema("Apellidos completos")]
        public string Apellidos { get; set; }

        [JsonPropertyName("idGenero")]
        [SwaggerSchema("Género")]
        public int IdGenero { get; set; }

        [JsonPropertyName("correoElectronico")]
        [SwaggerSchema("Correo electronico")]
        public string Correo_Electronico { get; set; }

        [JsonPropertyName("fechaNacimiento")]
        [SwaggerSchema("Fecha de nacimiento")]
        public DateTime Fecha_Nacimiento { get; set; }

        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del cliente.")]
        public string Celular { get; set; }

        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección del cliente.")]
        public string Direccion { get; set; }

        [JsonPropertyName("contrasenia")]
        [SwaggerSchema("Contraseña")]
        public string Contrasenia { get; set; }
    }
}
