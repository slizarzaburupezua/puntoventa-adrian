using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request
{
    public class RegistrarCategoriaRequest
    {
        [JsonPropertyName("destinationTimeZoneIdRegistro")]
        [SwaggerSchema("Localización del registro")]
        public string DestinationTimeZoneIdRegistro { get; set; }

        [JsonPropertyName("idUsuario")]
        [SwaggerSchema("Identificador del Usuario.")]
        public Guid IdUsuarioGuid { get; set; }

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


    }
}
