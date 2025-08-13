using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request
{
    public class RegistrarClienteRequest
    {
        [JsonPropertyName("destinationTimeZoneIdRegistro")]
        [SwaggerSchema("Localización del registro")]
        public string DestinationTimeZoneIdRegistro { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("idTipoDocumento")]
        [SwaggerSchema("Identificador del tipo del documento del cliente")]
        public int IdTipoDocumento { get; set; }

        [JsonPropertyName("idGenero")]
        [SwaggerSchema("Identificador del género del cliente")]
        public int IdGenero { get; set; }

        [JsonPropertyName("numeroDocumento")]
        [SwaggerSchema("Número de documento del cliente")]
        public string NumeroDocumento { get; set; }

        [JsonPropertyName("nombres")]
        [SwaggerSchema("Nombres del cliente.")]
        public string Nombres { get; set; }

        [JsonPropertyName("apellidos")]
        [SwaggerSchema("Apellidos del cliente.")]
        public string Apellidos { get; set; }

        [JsonPropertyName("correoElectronico")]
        [SwaggerSchema("Correo electrónico.")]
        public string CorreoElectronico { get; set; }

        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del cliente.")]
        public string Celular { get; set; }

        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección del cliente.")]
        public string Direccion { get; set; }

        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si se va a activar o desactivar.")]
        public bool Activo { get; set; }
    }
}
