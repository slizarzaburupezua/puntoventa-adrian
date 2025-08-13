using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response
{
    public class MonedaDTO
    {
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la moneda")]
        public int Id { get; set; }

        [JsonPropertyName("regionIsoDosLetras")]
        [SwaggerSchema("Dos letras de la moneda")]
        public string Region_iso_dos_letras { get; set; }

        [JsonPropertyName("regionIsoTresLetras")]
        [SwaggerSchema("Tres letras de la moneda")]
        public string Region_iso_tres_letras { get; set; }

        [JsonPropertyName("codigoMoneda")]
        [SwaggerSchema("Codigo de la moneda")]
        public string Codigo_moneda { get; set; }

        [JsonPropertyName("lenguajeCodigo")]
        [SwaggerSchema("Lenguaje Codigo de la moneda")]
        public string Lenguaje_codigo { get; set; }

        [JsonPropertyName("lenguajeDescripcion")]
        [SwaggerSchema("Lenguaje descripcion de la moneda")]
        public string Lenguaje_descripcion { get; set; }

        [JsonPropertyName("cultureInfo")]
        [SwaggerSchema("Culture informacion de la moneda")]
        public string Cultereinfo { get; set; }

        [JsonPropertyName("pais")]
        [SwaggerSchema("Pais de la moneda")]
        public string Pais { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripcion de la moneda")]
        public string Descripcion { get; set; }

        [JsonPropertyName("simbolo")]
        [SwaggerSchema("Simbolo de la moneda")]
        public string Simbolo { get; set; }

    }
}
