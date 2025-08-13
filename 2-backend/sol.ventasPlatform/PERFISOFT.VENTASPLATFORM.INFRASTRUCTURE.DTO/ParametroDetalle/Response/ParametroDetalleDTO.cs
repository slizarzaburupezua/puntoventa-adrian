using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Response
{
    public class ParametroDetalleDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del detalle de parámetro.")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("idParametro")]
        [SwaggerSchema("Identificador del parámetro padre.")]
        public int Id_Parametro { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("paraKey")]
        [SwaggerSchema("Clave principal del parámetro.")]
        public string Para_Key { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("subParaKey")]
        [SwaggerSchema("Clave secundaria del parámetro.")]
        public string Sub_Para_Key { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre del parámetro.")]
        public string Nombre { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción del parámetro.")]
        public string Descripcion { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("tipoCampo")]
        [SwaggerSchema("Tipo de dato del parámetro.")]
        public string TipoCampo { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("orden")]
        [SwaggerSchema("Orden de visualización.")]
        public int? Orden { get; set; }

        [Display(Order = 8)]
        [JsonPropertyName("svalor1")]
        [SwaggerSchema("Valor carácter 1.")]
        public string Svalor1 { get; set; }

        [Display(Order = 9)]
        [JsonPropertyName("svalor2")]
        [SwaggerSchema("Valor carácter 2.")]
        public string Svalor2 { get; set; }

        [Display(Order = 10)]
        [JsonPropertyName("svalor3")]
        [SwaggerSchema("Valor carácter 3.")]
        public string Svalor3 { get; set; }

        [Display(Order = 11)]
        [JsonPropertyName("dvalor1")]
        [SwaggerSchema("Valor decimal 1.")]
        public decimal? Dvalor1 { get; set; }

        [Display(Order = 12)]
        [JsonPropertyName("dvalor2")]
        [SwaggerSchema("Valor decimal 2.")]
        public decimal? Dvalor2 { get; set; }

        [Display(Order = 13)]
        [JsonPropertyName("dvalor3")]
        [SwaggerSchema("Valor decimal 3.")]
        public decimal? Dvalor3 { get; set; }

        [Display(Order = 14)]
        [JsonPropertyName("fvalor1")]
        [SwaggerSchema("Valor fecha 1.")]
        public DateTime? Fvalor1 { get; set; }

        [Display(Order = 15)]
        [JsonPropertyName("fvalor2")]
        [SwaggerSchema("Valor fecha 2.")]
        public DateTime? Fvalor2 { get; set; }

        [Display(Order = 16)]
        [JsonPropertyName("fvalor3")]
        [SwaggerSchema("Valor fecha 3.")]
        public DateTime? Fvalor3 { get; set; }

        [Display(Order = 17)]
        [JsonPropertyName("bvalor1")]
        [SwaggerSchema("Valor booleano 1.")]
        public bool? Bvalor1 { get; set; }

        [Display(Order = 18)]
        [JsonPropertyName("bvalor2")]
        [SwaggerSchema("Valor booleano 2.")]
        public bool? Bvalor2 { get; set; }

        [Display(Order = 19)]
        [JsonPropertyName("bvalor3")]
        [SwaggerSchema("Valor booleano 3.")]
        public bool? Bvalor3 { get; set; }

        [Display(Order = 20)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Indica si está activo.")]
        public bool Activo { get; set; }

        [Display(Order = 21)]
        [JsonPropertyName("estado")]
        [SwaggerSchema("Estado del parámetro.")]
        public bool Estado { get; set; }

        [Display(Order = 22)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro.")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Order = 23)]
        [JsonPropertyName("fechaActualizacion")]
        [SwaggerSchema("Fecha de última actualización.")]
        public DateTime? Fecha_Actualizacion { get; set; }

        [Display(Order = 24)]
        [JsonPropertyName("fechaAnulacion")]
        [SwaggerSchema("Fecha de anulación.")]
        public DateTime? Fecha_Anulacion { get; set; }

    }
}
