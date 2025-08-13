using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request
{
    public class ObtenerClienteRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("lstTipoDocumento")]
        [SwaggerSchema("Tipos de documentos")]
        public int[] LstTipoDocumento { get; set; }

        [JsonPropertyName("lstGenero")]
        [SwaggerSchema("Genero")]
        public int[] LstGenero { get; set; }

        [JsonPropertyName("numeroDocumento")]
        [SwaggerSchema("Número de documento del cliente")]
        public string NumeroDocumento { get; set; }

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

        [JsonPropertyName("correoElectronico")]
        [SwaggerSchema("Correo electrónico.")]
        public string CorreoElectronico { get; set; }

        [JsonPropertyName("fechaRegistroInicio")]
        [SwaggerSchema("Feha inicio del registro del producto")]
        public DateTime? FechaRegistroInicio { get; set; }

        [JsonPropertyName("fechaRegistroFin")]
        [SwaggerSchema("Feha fin del registro del producto")]
        public DateTime? FechaRegistroFin { get; set; }
    }
}
