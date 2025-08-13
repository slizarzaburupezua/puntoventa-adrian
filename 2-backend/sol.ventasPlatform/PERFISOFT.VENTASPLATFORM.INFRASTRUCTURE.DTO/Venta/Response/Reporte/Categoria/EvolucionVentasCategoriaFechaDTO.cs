using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria
{
    public class EvolucionVentasCategoriaFechaDTO
    {
        [JsonPropertyName("nombreCategoria")]
        [SwaggerSchema("Nombre de la categoría")]
        public string NombreCategoria { get; set; }

        [JsonPropertyName("colorCategoria")]
        [SwaggerSchema("Color de la categoría")]
        public string ColorCategoria { get; set; }

        [JsonPropertyName("datosVentasAgrupados")]
        [SwaggerSchema("Datos de la venta")]
        public List<VentasAnalisisAgrupadosCategoriasDTO> DatosVentasAgrupados { get; set; }
    }
}
