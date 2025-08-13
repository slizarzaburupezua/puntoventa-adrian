using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response
{
    public class ObtenerColaboradoresActivosDTO
    {
        [Display(Order = 1)]
        [JsonPropertyName("nombres")]
        [SwaggerSchema("Nombres del Usuario.")]
        public string Nombres { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("apellidos")]
        [SwaggerSchema("Apellidos del Usuario.")]
        public string Apellidos { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("correoElectronico")]
        [SwaggerSchema("Correo Electrónico del Usuario.")]
        public string CorreoElectronico { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del Usuario.")]
        public string Celular { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("urlFoto")]
        [SwaggerSchema("Url de la foto")]
        public string UrlFoto { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("nombreRol")]
        [SwaggerSchema("Nombre del rol")]
        public string NombreRol { get; set; }
    }
}
