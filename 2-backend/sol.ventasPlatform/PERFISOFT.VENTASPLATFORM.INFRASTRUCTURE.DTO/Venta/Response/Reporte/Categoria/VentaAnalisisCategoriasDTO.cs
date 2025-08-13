using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria
{
    public class VentaAnalisisCategoriasDTO
    {
        [JsonPropertyName("lstEvolucionVentasCategoriaFecha")]
        [SwaggerSchema("Evolucin de las ventas por categorias y fechas")]
        public List<EvolucionVentasCategoriaFechaDTO> LstEvolucionVentasCategoriaFecha { get; set; }

        [JsonPropertyName("evolucionVentasFecha")]
        [SwaggerSchema("Evolucion de las ventas por fechas")]
        public EvolucionVentasFechaDTO EvolucionVentasFecha { get; set; }

        [JsonPropertyName("distribucionVentasCategoria")]
        [SwaggerSchema("Montos de la Categorias de ventas con su porcentaje")]
        public DistribucionVentasCategoriaDTO DistribucionVentasCategoria { get; set; }

        [JsonPropertyName("topDiezCategoriasVentas")]
        [SwaggerSchema("Las 10 categorias con más ventas segun la fecha filtrada")]
        public VentasCategoriasTopDiezDTO TopDiezCategoriasVentas { get; set; }

    }
}
