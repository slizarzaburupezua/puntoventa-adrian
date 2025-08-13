using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response
{
    public class GeneroDTO
    {
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del genero")]
        public int Id { get; set; }

        [JsonPropertyName("codigo")]
        [SwaggerSchema("Codigo del genero")]
        public string Codigo { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripcion del genero")]
        public string Descripcion { get; set; }

        [JsonPropertyName("orden")]
        [SwaggerSchema("Orden del genero")]
        public int Orden { get; set; }

        [JsonPropertyName("estado")]
        [SwaggerSchema("Estado del genero")]
        public bool Estado { get; set; }

        [JsonPropertyName("Motivo de anulacion")]
        [SwaggerSchema("Motivo de anulacion del genero")]
        public string? Motivo_anulacion { get; set; }

        [JsonPropertyName("Fecha registro")]
        [SwaggerSchema("Fecha de registro del genero")]
        public DateTime Fecha_registro { get; set; }


        [JsonPropertyName("Fecha actualizacion")]
        [SwaggerSchema("Fecha de actualizacion del genero")]
        public DateTime? Fecha_actualizacion { get; set; }


        [JsonPropertyName("Fecha anulacion")]
        [SwaggerSchema("Fecha de anulacion del genero")]
        public DateTime? Fecha_anulacion { get; set; }

    }
}
