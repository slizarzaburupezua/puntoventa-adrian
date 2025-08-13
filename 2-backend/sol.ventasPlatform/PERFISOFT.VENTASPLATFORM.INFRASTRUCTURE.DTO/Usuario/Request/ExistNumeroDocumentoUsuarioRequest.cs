using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request
{
    public class ExistNumeroDocumentoUsuarioRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("numeroDocumento")]
        [SwaggerSchema("Núm del usuario.")]
        public string NumeroDocumento { get; set; }
    }
}
