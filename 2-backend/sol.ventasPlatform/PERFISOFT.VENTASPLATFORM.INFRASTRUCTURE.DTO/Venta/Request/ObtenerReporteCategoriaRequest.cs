using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request
{
    public class ObtenerReporteCategoriaRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        [SwaggerSchema("Ubicación del usuario.")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario que está consultando.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("lstCategorias")]
        [SwaggerSchema("Lista de categorias")]
        public int[] LstCategorias { get; set; }

        [JsonPropertyName("fechaVentaInicio")]
        [SwaggerSchema("Feha venta inicio")]
        public DateTime? FechaVentaInicio { get; set; }

        [JsonPropertyName("fechaVentaFin")]
        [SwaggerSchema("Feha venta fin")]
        public DateTime? FechaVentaFin { get; set; }
    }
}
