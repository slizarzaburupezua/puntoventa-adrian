using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response
{
    public class UsuarioIdDTO
    {
        [JsonPropertyName("idUsuarioGuid")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid Id_Usuario_Guid { get; set; }
    }
}
