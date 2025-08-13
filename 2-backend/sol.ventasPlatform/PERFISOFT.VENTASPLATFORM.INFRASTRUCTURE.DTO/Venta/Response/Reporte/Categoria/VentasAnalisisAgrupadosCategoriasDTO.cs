using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria
{
    public class VentasAnalisisAgrupadosCategoriasDTO
    {
        [JsonPropertyName("montoVentaTotal")]
        [SwaggerSchema("Valor de la venta Total")]
        public decimal MontoVentaTotal { get; set; }

        [JsonPropertyName("fechaVenta")]
        [SwaggerSchema("Fecha de la venta")]
        public DateTime FechaVenta { get; set; }
    }
}
