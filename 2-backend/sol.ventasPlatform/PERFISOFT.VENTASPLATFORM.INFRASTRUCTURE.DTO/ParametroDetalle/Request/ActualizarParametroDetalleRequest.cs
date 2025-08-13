using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Request
{
    public class ActualizarParametroDetalleRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización del registro")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del parametro detalle")]
        public int Id { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("paraKey")]
        [SwaggerSchema("Tipo de parámetro")]
        public string ParaKey { get; set; }

        [JsonPropertyName("tipoCampo")]
        [SwaggerSchema("Tipo de campo del detalle del parámetro")]
        public string TipoCampo { get; set; }

        [JsonPropertyName("svalor1")]
        [SwaggerSchema("Nombre del Primer Valor")]
        public string? Svalor1 { get; set; }

        [JsonPropertyName("svalor2")]
        [SwaggerSchema("Nombre del Segundo Valor")]
        public string? Svalor2 { get; set; }

    }
}
