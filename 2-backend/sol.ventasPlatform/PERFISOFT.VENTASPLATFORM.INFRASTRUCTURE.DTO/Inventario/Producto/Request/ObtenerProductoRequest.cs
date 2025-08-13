using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request
{
    public class ObtenerProductoRequest
    {
        [JsonPropertyName("destinationTimeZoneId")]
        public string DestinationTimeZoneId { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuario { get; set; }

        [JsonPropertyName("codigo")]
        [SwaggerSchema("Código del producto.")]
        public string Codigo { get; set; }

        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre del producto.")]
        public string Nombre { get; set; }

        [JsonPropertyName("fechaRegistroInicio")]
        [SwaggerSchema("Feha inicio del registro del producto")]
        public DateTime? FechaRegistroInicio { get; set; }

        [JsonPropertyName("fechaRegistroFin")]
        [SwaggerSchema("Feha fin del registro del producto")]
        public DateTime? FechaRegistroFin { get; set; }

        [JsonPropertyName("precioCompraInicio")]
        [SwaggerSchema("Precio compra inicio del producto")]
        public decimal? PrecioCompraInicio { get; set; }

        [JsonPropertyName("precioCompraFin")]
        [SwaggerSchema("Precio compra fin del producto")]
        public decimal? PrecioCompraFin { get; set; }

        [JsonPropertyName("precioVentaInicio")]
        [SwaggerSchema("Precio venta inicio del producto")]
        public decimal? PrecioVentaInicio { get; set; }

        [JsonPropertyName("precioVentaFin")]
        [SwaggerSchema("Precio venta fin del producto")]
        public decimal? PrecioVentaFin { get; set; }

        [JsonPropertyName("lstCategorias")]
        [SwaggerSchema("Lista de categorias")]
        public int[] LstCategorias { get; set; }

        [JsonPropertyName("lstMarcas")]
        [SwaggerSchema("Lista de marcas")]
        public int[] LstMarcas { get; set; }
    }
}
