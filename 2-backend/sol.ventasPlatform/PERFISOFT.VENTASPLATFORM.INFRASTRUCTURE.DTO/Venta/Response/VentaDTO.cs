using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response
{
    public class VentaDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("idVenta")]
        [SwaggerSchema("Identificador de la venta")]
        public int IdVenta { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("numeroVenta")]
        [SwaggerSchema("Número de Venta")]
        public string NumeroVenta { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("nombreCliente")]
        [SwaggerSchema("Cliente a que se le hizo la venta")]
        public string NombreCliente { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("correoUsuario")]
        [SwaggerSchema("Correo Usuario que realizó la venta")]
        public string CorreoUsuario { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("fechaVenta")]
        [SwaggerSchema("Fecha de venta")]
        public DateTime FechaVenta { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("totalVenta")]
        [SwaggerSchema("Monto total de la venta")]
        public decimal TotalVenta { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("urlBoletaFactura")]
        [SwaggerSchema("Url de la BoletaVenta")]
        public string UrlBoletaFactura { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("estado")]
        [SwaggerSchema("Flag que indica si la venta se encuentra anulada o no")]
        public bool Estado { get; set; }


    }
}
