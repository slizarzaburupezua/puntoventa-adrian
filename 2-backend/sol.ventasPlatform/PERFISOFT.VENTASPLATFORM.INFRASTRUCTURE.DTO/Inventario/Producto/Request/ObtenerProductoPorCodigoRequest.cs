using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request
{
    public class ObtenerProductoPorCodigoRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("parametro")]
        [SwaggerSchema("Parametro de busqueda ya sea por nombre o por codigo.")]
        public string Parametro { get; set; }
    }
}
