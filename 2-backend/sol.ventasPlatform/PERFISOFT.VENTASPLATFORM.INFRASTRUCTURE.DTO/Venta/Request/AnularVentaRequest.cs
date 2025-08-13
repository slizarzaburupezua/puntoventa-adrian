using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request
{
    public class AnularVentaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la venta")]
        public int Id { get; set; }

        [JsonPropertyName("recuperarStock")]
        [SwaggerSchema("Flag que indica si se va a recuperar el stock")]
        public bool RecuperarStock { get; set; }
    }
}
