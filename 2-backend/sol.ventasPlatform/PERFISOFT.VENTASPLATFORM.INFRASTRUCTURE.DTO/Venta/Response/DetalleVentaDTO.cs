using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response
{
    public class DetalleVentaDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("idProducto")]
        [SwaggerSchema("Identificador del producto")]
        public int IdProducto { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("idVenta")]
        [SwaggerSchema("Identificador de la venta")]
        public int IdVenta { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("numeroVenta")]
        [SwaggerSchema("Número de la venta")]
        public string NumeroVenta { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("idCliente")]
        [SwaggerSchema("Identificador del cliente")]
        public int? IdCliente { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("nombreCliente")]
        [SwaggerSchema("Nombre completo del cliente")]
        public string? NombreCliente { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección del cliente")]
        public string? Direccion { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("urlBoletaFactura")]
        [SwaggerSchema("URL de la boleta o factura asociada a la venta")]
        public string? UrlBoletaFactura { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("fechaVenta")]
        [SwaggerSchema("Fecha en que se realizó la venta")]
        public DateTime FechaVenta { get; set; }

        [Display(Order = 8)]
        [JsonPropertyName("urlFotoProducto")]
        [SwaggerSchema("URL de la foto del producto")]
        public string? UrlFotoProducto { get; set; }

        [Display(Order = 9)]
        [JsonPropertyName("nombreProducto")]
        [SwaggerSchema("Nombre del producto")]
        public string NombreProducto { get; set; }

        [Display(Order = 10)]
        [JsonPropertyName("colorProducto")]
        [SwaggerSchema("Color del producto")]
        public string ColorProducto { get; set; }

        [Display(Order = 11)]
        [JsonPropertyName("nombreCategoria")]
        [SwaggerSchema("Nombre de la categoría del producto")]
        public string NombreCategoria { get; set; }

        [Display(Order = 12)]
        [JsonPropertyName("colorCategoria")]
        [SwaggerSchema("Color de la categoría del producto")]
        public string ColorCategoria { get; set; }

        [Display(Order = 13)]
        [JsonPropertyName("nombreMarca")]
        [SwaggerSchema("Nombre de la marca del producto")]
        public string NombreMarca { get; set; }

        [Display(Order = 14)]
        [JsonPropertyName("colorMarca")]
        [SwaggerSchema("Color de la marca del producto")]
        public string ColorMarca { get; set; }

        [Display(Order = 15)]
        [JsonPropertyName("cantidad")]
        [SwaggerSchema("Cantidad del producto vendida")]
        public int Cantidad { get; set; }

        [Display(Order = 16)]
        [JsonPropertyName("precioProducto")]
        [SwaggerSchema("Precio de venta unitario del producto")]
        public decimal PrecioProducto { get; set; }

        [Display(Order = 17)]
        [JsonPropertyName("precioTotal")]
        [SwaggerSchema("Precio total de la venta del producto")]
        public decimal PrecioTotal { get; set; }

        [Display(Order = 18)]
        [JsonPropertyName("notaAdicional")]
        [SwaggerSchema("Nota adicional de la venta")]
        public string NotaAdicional { get; set; }
    }
}
