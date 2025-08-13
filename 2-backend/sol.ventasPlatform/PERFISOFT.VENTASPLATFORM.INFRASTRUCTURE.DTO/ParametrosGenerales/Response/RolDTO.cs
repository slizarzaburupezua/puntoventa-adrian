using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response
{
    public class RolDTO
    {
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del rol")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre del rol")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripcion del rol")]
        public string Descripcion { get; set; }
    }
}
