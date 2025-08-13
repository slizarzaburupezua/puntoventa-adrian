using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request
{
    public class RegistrarVentaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdRegistro")]
        [SwaggerSchema("Localización del registro")]
        public string DestinationTimeZoneIdRegistro { get; set; }

        [JsonPropertyName("flgEnviarComprobante")]
        [SwaggerSchema("Flag que indica si se le enviará el comprobante al usuario")]
        public bool FlgEnviarComprobante { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("idCliente")]
        [SwaggerSchema("Identificador del Cliente.")]
        public int? IdCliente { get; set; }

        [JsonPropertyName("notaAdicional")]
        [SwaggerSchema("Nota Adicional")]
        public string NotaAdicional { get; set; }

        [JsonPropertyName("fechaRegistroVenta")]
        [SwaggerSchema("Feha registro de la venta")]
        public DateTime FechaRegistroVenta { get; set; }

        [JsonPropertyName("lstDetalleVenta")]
        [SwaggerSchema("Detalle de la venta.")]
        public List<RegistrarDetalleVentaRequest> LstDetalleVenta { get; set; }
    }
}
