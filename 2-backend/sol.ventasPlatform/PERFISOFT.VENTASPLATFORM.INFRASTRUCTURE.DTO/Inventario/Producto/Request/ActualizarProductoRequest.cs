using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request
{
    public class ActualizarProductoRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del producto")]
        public int Id { get; set; }

        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("idCategoria")]
        [SwaggerSchema("Identificador de la categoria del producto.")]
        public int Id_Categoria { get; set; }

        [JsonPropertyName("idMarca")]
        [SwaggerSchema("Identificador de la marca del producto.")]
        public int Id_Marca { get; set; }

        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre del producto.")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción del producto")]
        public string Descripcion { get; set; }

        [JsonPropertyName("color")]
        [SwaggerSchema("Color del producto")]
        public string Color { get; set; }

        [JsonPropertyName("precioCompra")]
        [SwaggerSchema("Precio de compra del producto")]
        public decimal PrecioCompra { get; set; }

        [JsonPropertyName("precioVenta")]
        [SwaggerSchema("Precio de venta del producto")]
        public decimal PrecioVenta { get; set; }

        [JsonPropertyName("stock")]
        [SwaggerSchema("Stock del producto")]
        public int Stock { get; set; }

        [JsonPropertyName("nombreArchivo")]
        [SwaggerSchema("Nombre del archivo de la foto.")]
        public string NombreArchivo { get; set; }

        [JsonPropertyName("foto")]
        [SwaggerSchema("Foto del producto.")]
        public string Foto { get; set; }

        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si el producto se encuentra activo")]
        public bool Activo { get; set; }
    }
}
