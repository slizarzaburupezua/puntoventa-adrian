using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response
{
    public class MenuRolDTO
    {
        [JsonPropertyName("padre")]
        [SwaggerSchema("Padre del menu textro")]
        public string Padre_texto { get; set; }

        [JsonPropertyName("hijoTexto")]
        [SwaggerSchema("Hijo del menu texto")]
        public string Hijo_texto { get; set; }

        [JsonPropertyName("titulo")]
        [SwaggerSchema("Titulo del menu")]
        public string Titulo { get; set; }

        [JsonPropertyName("tipo")]
        [SwaggerSchema("Tipo de menu")]
        public string Tipo { get; set; }

        [JsonPropertyName("icono")]
        [SwaggerSchema("Icono de menu")]
        public string Icono { get; set; }

        [JsonPropertyName("flgEnlaceExterno")]
        [SwaggerSchema("Enlace de menu")]
        public bool Flg_enlace_externo { get; set; }

        [JsonPropertyName("flgMenuHijo")]
        [SwaggerSchema("Flg menu hijo")]
        public bool Flg_menu_hijo { get; set; }

        [JsonPropertyName("ruta")]
        [SwaggerSchema("Ruta de menu")]
        public string Ruta { get; set; }

        [JsonPropertyName("orden")]
        [SwaggerSchema("Orden del menu")]
        public int Orden { get; set; }
    }
}
