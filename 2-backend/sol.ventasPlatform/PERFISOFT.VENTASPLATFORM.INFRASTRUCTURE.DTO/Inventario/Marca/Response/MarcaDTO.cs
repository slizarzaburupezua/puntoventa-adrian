using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Response
{
    public class MarcaDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la marca")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la marca")]
        public string Nombre { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripcion de la marca")]
        public string Descripcion { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("color")]
        [SwaggerSchema("Color de la marca")]
        public string Color { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro de la marca")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("fechaActualizacion")]
        [SwaggerSchema("Última Fecha de modificación de la marca.")]
        public DateTime? Fecha_Actualizacion { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si la marca se encuentra activo.")]
        public bool Activo { get; set; }
    }
}
