using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria
{
    public class VentaAnalisisCategoriasAgrupadosDTO
    {
        [JsonPropertyName("nombreCategoria")]
        [SwaggerSchema("Nombre de la categoría")]
        public string NombreCategoria { get; set; }

        [JsonPropertyName("colorCategoria")]
        [SwaggerSchema("Color de la categoría")]
        public string ColorCategoria { get; set; }

        [JsonPropertyName("fechaVenta")]
        [SwaggerSchema("Fecha de la venta")]
        public DateTime FechaVentaAgrupada { get; set; }

        [JsonPropertyName("montoTotal")]
        [SwaggerSchema("Suma total de las ventas agrupados por la categoria")]
        public decimal MontoTotal { get; set; }

        [JsonPropertyName("datosVentas")]
        [SwaggerSchema("Datos de la venta")]
        public List<VentasAnalisisAgrupadosCategoriasDTO> Datos { get; set; }
    }
}
