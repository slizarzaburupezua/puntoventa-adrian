using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request
{
    public class RegistrarMarcaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdRegistro")]
        [SwaggerSchema("Localización del registro")]
        public string DestinationTimeZoneIdRegistro { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la marca.")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción de la marca")]
        public string Descripcion { get; set; }

        [JsonPropertyName("color")]
        [SwaggerSchema("Color de la marca")]
        public string Color { get; set; }

    }
}
