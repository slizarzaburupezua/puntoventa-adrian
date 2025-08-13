using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class ObtenerUsuarioRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("lstGenero")]
        [SwaggerSchema("Generos")]
        public int[] LstGenero { get; set; }

        [JsonPropertyName("lstEstadoCuenta")]
        [SwaggerSchema("Estados de Cuenta")]
        public int[] LstEstadosCuenta { get; set; }

        [JsonPropertyName("nombres")]
        [SwaggerSchema("Nombres del cliente.")]
        public string Nombres { get; set; }

        [JsonPropertyName("apellidos")]
        [SwaggerSchema("Apellidos del cliente.")]
        public string Apellidos { get; set; }

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
