using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request
{
    public class ObtenerClientePorNumDocumentoCorreoRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("parametro")]
        [SwaggerSchema("Parametro de busqueda ya sea por Número de dócumento o por Correo.")]
        public string Parametro { get; set; }
    }
}
