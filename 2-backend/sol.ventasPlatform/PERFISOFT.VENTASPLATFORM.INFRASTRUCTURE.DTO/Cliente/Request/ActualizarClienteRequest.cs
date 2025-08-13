using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request
{
    public class ActualizarClienteRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del cliente")]
        public int Id { get; set; }

        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("nombres")]
        [SwaggerSchema("Nombres del cliente.")]
        public string Nombres { get; set; }

        [JsonPropertyName("apellidos")]
        [SwaggerSchema("Apellidos del cliente.")]
        public string Apellidos { get; set; }

        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del cliente.")]
        public string Celular { get; set; }

        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección del cliente.")]
        public string Direccion { get; set; }
    }
}
