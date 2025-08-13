using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response
{
    public class MetodoPagoDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del método de pago")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción del método de pago")]
        public string Descripcion { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("color")]
        [SwaggerSchema("Color del método de pago")]
        public string Color { get; set; }
    }
}
