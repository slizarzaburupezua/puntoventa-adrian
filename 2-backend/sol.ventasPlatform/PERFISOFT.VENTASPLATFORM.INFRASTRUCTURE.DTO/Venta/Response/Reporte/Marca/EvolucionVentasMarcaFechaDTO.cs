using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca
{
    public class EvolucionVentasMarcaFechaDTO
    {
        [JsonPropertyName("nombreMarca")]
        [SwaggerSchema("Nombre de la marca")]
        public string NombreMarca { get; set; }

        [JsonPropertyName("colorMarca")]
        [SwaggerSchema("Color de la marca")]
        public string ColorMarca { get; set; }

        [JsonPropertyName("datosVentasAgrupados")]
        [SwaggerSchema("Datos de la venta")]
        public List<VentasAnalisisAgrupadosMarcasDTO> DatosVentasAgrupados { get; set; }
    }
}
