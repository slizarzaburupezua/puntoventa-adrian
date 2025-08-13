using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Parametro.Response
{
    public class ParametroDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del parámetro.")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre del parámetro.")]
        public string Nombre { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción del parámetro.")]
        public string Descripcion { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Indica si está activo.")]
        public bool Activo { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("estado")]
        [SwaggerSchema("Estado del parámetro.")]
        public bool Estado { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro del parámetro.")]
        public DateTime Fecha_Registro { get; set; }

    }
}
