using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Response
{
    public class ProductoDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del producto")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("codigo")]
        [SwaggerSchema("Código del producto.")]
        public string Codigo { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre del producto.")]
        public string Nombre { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción del producto")]
        public string Descripcion { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("color")]
        [SwaggerSchema("Color del producto")]
        public string Color { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("precioCompra")]
        [SwaggerSchema("Precio de compra del producto")]
        public decimal Precio_Compra { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("precioVenta")]
        [SwaggerSchema("Precio de venta del producto")]
        public decimal Precio_Venta { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("stock")]
        [SwaggerSchema("Stock del producto")]
        public int Stock { get; set; }

        [Display(Order = 8)]
        [JsonPropertyName("urlFoto")]
        [SwaggerSchema("Url foto del producto")]
        public string UrlFoto { get; set; }

        [Display(Order = 9)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro del producto.")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Order = 10)]
        [JsonPropertyName("fechaActualizacion")]
        [SwaggerSchema("Última Fecha de modificación del producto.")]
        public DateTime? Fecha_Actualizacion { get; set; }

        [Display(Order = 11)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si el producto se encuentra activo.")]
        public bool Activo { get; set; }

        [Display(Order = 12)]
        [JsonPropertyName("categoria")]
        [SwaggerSchema("Categoría del producto")]
        public CategoriaDTO Categoria { get; set; }

        [Display(Order = 13)]
        [JsonPropertyName("marca")]
        [SwaggerSchema("Marca del producto")]
        public MarcaDTO MArca { get; set; }
    }
}
