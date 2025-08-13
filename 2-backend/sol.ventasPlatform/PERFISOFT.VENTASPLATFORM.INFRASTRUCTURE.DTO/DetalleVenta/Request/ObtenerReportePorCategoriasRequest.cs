using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.DetalleVenta.Request
{
    public class ObtenerReportePorCategoriasRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        [SwaggerSchema("Ubicación del usuario.")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("lstCategorias")]
        [SwaggerSchema("Lista de categorias")]
        public int[] LstCategorias { get; set; }

        [JsonPropertyName("fechaVentaInicio")]
        [SwaggerSchema("Rango de Feha venta inicio")]
        public DateTime? FechaVentaInicio { get; set; }

        [JsonPropertyName("fechaVentaFin")]
        [SwaggerSchema("Rango de Feha venta fin")]
        public DateTime? FechaVentaFin { get; set; }

    }
}
