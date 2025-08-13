using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request
{
    public class RegistrarProductoRequest
    {
        [JsonPropertyName("destinationTimeZoneIdRegistro")]
        [SwaggerSchema("Localización del registro")]
        public string DestinationTimeZoneIdRegistro { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("idCategoria")]
        [SwaggerSchema("Identificador de la categoria del producto.")]
        public int Id_Categoria { get; set; }

        [JsonPropertyName("idMarca")]
        [SwaggerSchema("Identificador de la marca del producto.")]
        public int Id_Marca { get; set; }

        [JsonPropertyName("codigo")]
        [SwaggerSchema("Código de la categoría.")]
        public string Codigo { get; set; }

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

        [JsonPropertyName("fotoBase64")]
        [SwaggerSchema("Foto del producto.")]
        public string FotoBase64 { get; set; }

        [JsonIgnore]
        public string IdFoto { get; set; }

        [JsonIgnore]
        public string UrlFoto { get; set; }
    }
}
