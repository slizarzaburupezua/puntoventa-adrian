using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Response
{
    public class CategoriaConConteoDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("idCategoria")]
        [SwaggerSchema("Identificador del producto")]
        public int IdCategoria { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la categoría")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Order = 2)]
        [JsonPropertyName("cantidadProductos")]
        [SwaggerSchema("Cantidad de productos pro la categoría")]
        public int CantidadProductos { get; set; }
    }
}
