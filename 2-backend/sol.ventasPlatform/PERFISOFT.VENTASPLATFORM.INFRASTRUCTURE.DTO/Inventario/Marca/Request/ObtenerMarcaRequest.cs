using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request
{
    public class ObtenerMarcaRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la marca")]
        public string Nombre { get; set; }
    }
}
