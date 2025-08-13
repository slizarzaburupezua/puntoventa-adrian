using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca
{
    public class VentaAnalisisMarcasAgrupadosDTO
    {
        [JsonPropertyName("nombreMarca")]
        [SwaggerSchema("Nombre de la marca")]
        public string NombreMarca { get; set; }

        [JsonPropertyName("colorMarca")]
        [SwaggerSchema("Color de la marca")]
        public string ColorMarca { get; set; }

        [JsonPropertyName("fechaVenta")]
        [SwaggerSchema("Fecha de la venta")]
        public DateTime FechaVentaAgrupada { get; set; }

        [JsonPropertyName("montoTotal")]
        [SwaggerSchema("Suma total de las ventas agrupados por la marca")]
        public decimal MontoTotal { get; set; }

        [JsonPropertyName("datosVentas")]
        [SwaggerSchema("Datos de la venta")]
        public List<VentasAnalisisAgrupadosMarcasDTO> Datos { get; set; }
    }
}
