using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request
{
    public class RegistrarDetalleVentaRequest
    {
        [JsonPropertyName("idProducto")]
        [SwaggerSchema("Identificador del producto.")]
        public int IdProducto { get; set; }

        [JsonPropertyName("urlFotoProducto")]
        [SwaggerSchema("Url foto del producto.")]
        public string UrlFotoProducto { get; set; }

        [JsonPropertyName("nombreProducto")]
        [SwaggerSchema("Nombre del producto.")]
        public string NombreProducto { get; set; }

        [JsonPropertyName("colorProducto")]
        [SwaggerSchema("Color del producto")]
        public string ColorProducto { get; set; }

        [JsonPropertyName("nombreCategoria")]
        [SwaggerSchema("Nombre de categoría del producto")]
        public string NombreCategoria { get; set; }

        [JsonPropertyName("colorCategoria")]
        [SwaggerSchema("Color de la categoría del producto")]
        public string ColorCategoria { get; set; }

        [JsonPropertyName("nombreMarca")]
        [SwaggerSchema("Nombre de la marca del producto")]
        public string NombreMarca { get; set; }

        [JsonPropertyName("colorMarca")]
        [SwaggerSchema("Color de la marca del producto")]
        public string ColorMarca { get; set; }

        [JsonPropertyName("cantidad")]
        [SwaggerSchema("Cantidad del producto.")]
        public int Cantidad { get; set; }

        [JsonPropertyName("precioCompra")]
        [SwaggerSchema("Precio compra actual del producto.")]
        public decimal PrecioCompra { get; set; }

        [JsonPropertyName("precioVenta")]
        [SwaggerSchema("Precio venta actual del producto.")]
        public decimal PrecioVenta { get; set; }

    }
}
