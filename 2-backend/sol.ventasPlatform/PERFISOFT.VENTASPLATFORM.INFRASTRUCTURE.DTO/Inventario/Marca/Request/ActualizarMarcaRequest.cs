using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request
{
    public class ActualizarMarcaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la marca")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la marca.")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción de la marca")]
        public string Descripcion { get; set; }

        [JsonPropertyName("color")]
        [SwaggerSchema("Color de la marca")]
        public string Color { get; set; }

        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si la marca se encuentra activo")]
        public bool Activo { get; set; }
    }
}
