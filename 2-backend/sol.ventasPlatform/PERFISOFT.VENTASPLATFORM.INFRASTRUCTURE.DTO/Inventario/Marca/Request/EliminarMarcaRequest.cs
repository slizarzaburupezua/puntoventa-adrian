using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request
{
    public class EliminarMarcaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la marca.")]
        public int Id { get; set; }

        [JsonPropertyName("motivoAnulacion")]
        [SwaggerSchema("Motivo anulación de la marca")]
        public string MotivoAnulacion { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }
    }
}
