using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request
{
    public class ActualizarCategoriaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdActualizacion")]
        [SwaggerSchema("Localización de la actualización")]
        public string DestinationTimeZoneIdActualizacion { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador de la medida")]
        public int Id { get; set; }

        [JsonPropertyName("idMedida")]
        [SwaggerSchema("Identificador de la medida de la categoria.")]
        public int Id_Medida { get; set; }

        [JsonPropertyName("nombre")]
        [SwaggerSchema("Nombre de la categoría.")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        [SwaggerSchema("Descripción de la categoria")]
        public string Descripcion { get; set; }

        [JsonPropertyName("color")]
        [SwaggerSchema("Color de la categoria")]
        public string Color { get; set; }

        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si la categoria se encuentra activo")]
        public bool Activo { get; set; }
    }
}
