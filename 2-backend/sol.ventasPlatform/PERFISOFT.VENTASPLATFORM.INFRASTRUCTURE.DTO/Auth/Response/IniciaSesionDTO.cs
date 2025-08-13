using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Response
{
    public class IniciaSesionDTO
    {
        [SwaggerSchema("Código del error o de la advertencia.")]
        [JsonPropertyName("response")]
        public ResponseDTO Response { get; set; }

        [SwaggerSchema("Menu disponible para el usuario en base a su rol")]
        [JsonPropertyName("menuRol")]
        public List<MenuRolDTO> MenuRol { get; set; }

        [SwaggerSchema("Moneda y localización del negocio")]
        [JsonPropertyName("moneda")]
        public MonedaDTO Moneda { get; set; }
    }
}
