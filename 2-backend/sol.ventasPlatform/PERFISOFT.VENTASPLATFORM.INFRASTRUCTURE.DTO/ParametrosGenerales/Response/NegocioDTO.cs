using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response
{
    public class NegocioDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del negocio")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("idMoneda")]
        [SwaggerSchema("Identificador de la moneda del negocio")]
        public int Id_moneda { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("razonSocial")]
        [SwaggerSchema("Razon del negocio")]
        public string Razon_social { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("ruc")]
        [SwaggerSchema("Ruc del negocio")]
        public string Ruc { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("direccion")]
        [SwaggerSchema("Direccion del negocio")]
        public string Direccion { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del negocio")]
        public string Celular { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("correo")]
        [SwaggerSchema("Correo del negocio")]
        public string Correo_electronico { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("urlLogoBoleta")]
        [SwaggerSchema("Correo del negocio")]
        public string Urlfoto { get; set; }
         
        [Display(Order = 8)]
        [JsonPropertyName("colorBoletaFactura")]
        [SwaggerSchema("Color boleta cabecera")]
        public string Color_Boleta_Factura { get; set; }
 
        [Display(Order = 9)]
        [JsonPropertyName("formatoImpresion")]
        [SwaggerSchema("Fomato de la impresión de la boleta o factura")]
        public string Formato_Impresion { get; set; }

        [Display(Order = 10)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro del negocio")]
        public DateTime Fecha_registro { get; set; }

        [Display(Order = 11)]
        [JsonPropertyName("moneda")]
        [SwaggerSchema("Moneda y localización del negocio")]
        public MonedaDTO Moneda { get; set; }
    }
}
