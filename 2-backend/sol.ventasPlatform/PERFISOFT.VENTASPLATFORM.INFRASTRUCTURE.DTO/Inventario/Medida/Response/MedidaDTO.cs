using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Medida.Response
{
    public class MedidaDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la medida")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la medida.")]
        public string Nombre { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripcion de medida.")]
        public string Descripcion { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("abreviatura")]
        [SwaggerSchema("Abreviatura de la medida.")]
        public string Abreviatura { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("equivalente")]
        [SwaggerSchema("Equivalente de la medida.")]
        public string Equivalente { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("valor")]
        [SwaggerSchema("Valor de la medida.")]
        public int Valor { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si la medida se encuentra activo.")]
        public bool Activo { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro de la medida del ingreso.")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("fechaActualizacion")]
        [SwaggerSchema("Última Fecha de modificación de la medida del ingreso.")]
        public DateTime? Fecha_Actualizacion { get; set; }

    }
}
