using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Request
{
    public class ActualizarNegocioRequest
    {
        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("idMoneda")]
        [SwaggerSchema("Identificador de la moneda")]
        public int IdMoneda { get; set; }

        [JsonPropertyName("codMoneda")]
        [SwaggerSchema("Código de la moneda")]
        public string CodMoneda { get; set; }

        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("razonSocial")]
        [SwaggerSchema("Razón Social de la empresa.")]
        public string RazonSocial { get; set; }

        [JsonPropertyName("ruc")]
        [SwaggerSchema("RUC de la empresa.")]
        public string Ruc { get; set; }

        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección de la empresa.")]
        public string Direccion { get; set; }

        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular de la empresa.")]
        public string Celular { get; set; }

        [JsonPropertyName("correo")]
        [SwaggerSchema("Correo de la empresa.")]
        public string Correo { get; set; }

        [JsonPropertyName("nombreArchivo")]
        [SwaggerSchema("Nombre del archivo de la foto.")]
        public string? NombreArchivo { get; set; }

        [JsonPropertyName("foto")]
        [SwaggerSchema("Foto de la boleta del negocio.")]
        public string? Foto { get; set; }

        [JsonPropertyName("colorBoleta")]
        [SwaggerSchema("Color de la boleta")]
        public string? ColorBoleta { get; set; }
 
        [JsonPropertyName("codFormatoImpresion")]
        [SwaggerSchema("Código fomato de la impresión de la boleta o factura")]
        public string CodFormatoImpresion { get; set; }
    }
}
