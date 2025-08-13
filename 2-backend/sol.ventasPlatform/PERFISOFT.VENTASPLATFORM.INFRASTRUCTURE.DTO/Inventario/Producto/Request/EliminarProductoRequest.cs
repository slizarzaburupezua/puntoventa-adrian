using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request
{
    public class EliminarProductoRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del producto.")]
        public int Id { get; set; }

        [JsonPropertyName("motivoAnulacion")]
        [SwaggerSchema("Motivo anulación del producto")]
        public string MotivoAnulacion { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }
    }
}
