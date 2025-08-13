using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Medida.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Response
{
    public class CategoriaDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la categoria")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la categoria")]
        public string Nombre { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripcion de la categoria")]
        public string Descripcion { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("color")]
        [SwaggerSchema("Color de la categoria")]
        public string Color { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro de la categoria.")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("fechaActualizacion")]
        [SwaggerSchema("Última Fecha de modificación de la categoria.")]
        public DateTime? Fecha_Actualizacion { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si el ingreso se encuentra activo.")]
        public bool Activo { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("medida")]
        [SwaggerSchema("Información sobre la categoria")]
        public MedidaDTO Medida { get; set; }

    }
}
