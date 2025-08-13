using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response
{
    public class TipoDocumentoDTO
    {
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del documento")]
        public int Id { get; set; }

        [JsonPropertyName("codigo")]
        [SwaggerSchema("codigo del documento")]
        public string Codigo { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripcion del documento")]
        public string descripcion { get; set; }

        [JsonPropertyName("orden")]
        [SwaggerSchema("Orden del documento")]
        public int Orden { get; set; }

        [JsonPropertyName("estado")]
        [SwaggerSchema("Estado del documento")]
        public bool Estado { get; set; }

        [JsonPropertyName("Motivo de anulacion")]
        [SwaggerSchema("Motivo de anulacion del documento")]
        public string? Motivo_anulacion { get; set; }

        [JsonPropertyName("Fecha registro")]
        [SwaggerSchema("Fecha de registro del documento")]
        public DateTime Fecha_registro { get; set; }


        [JsonPropertyName("Fecha actualizacion")]
        [SwaggerSchema("Fecha de actualizacion del documento")]
        public DateTime? Fecha_actualizacion { get; set; }


        [JsonPropertyName("Fecha anulacion")]
        [SwaggerSchema("Fecha de anulacion del documento")]
        public DateTime? Fecha_anulacion { get; set; }
    }
}
