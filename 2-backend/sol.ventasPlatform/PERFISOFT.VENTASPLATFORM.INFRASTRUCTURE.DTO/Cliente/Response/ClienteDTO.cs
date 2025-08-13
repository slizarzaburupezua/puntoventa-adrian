using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Response
{
    public class ClienteDTO
    {
        [Display(Order = 0)]
        [JsonPropertyName("id")]
        [SwaggerSchema("Identificador del cliente")]
        public int Id { get; set; }

        [Display(Order = 1)]
        [JsonPropertyName("idTipoDocumento")]
        [SwaggerSchema("Identificador del tipo del documento del cliente")]
        public int Id_Tipo_Documento { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("idGenero")]
        [SwaggerSchema("Identificador del género del cliente")]
        public int Id_Genero { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("numeroDocumento")]
        [SwaggerSchema("Número de documento del cliente")]
        public string Numero_Documento { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("nombres")]
        [SwaggerSchema("Nombres del cliente.")]
        public string Nombres { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("apellidos")]
        [SwaggerSchema("Apellidos del cliente.")]
        public string Apellidos { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("correoElectronico")]
        [SwaggerSchema("Correo electrónico.")]
        public string Correo_Electronico { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del cliente.")]
        public string Celular { get; set; }

        [Display(Order = 8)]
        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección del cliente.")]
        public string Direccion { get; set; }

        [Display(Order = 9)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha de registro del cliente.")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Order = 10)]
        [JsonPropertyName("fechaActualizacion")]
        [SwaggerSchema("Última Fecha de modificación del cliente.")]
        public DateTime? Fecha_Actualizacion { get; set; }

        [Display(Order = 11)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si se va a activar o desactivar.")]
        public bool Activo { get; set; }

        [Display(Order = 12)]
        [JsonPropertyName("tipoDocumento")]
        [SwaggerSchema("Tipo de documento del cliente.")]
        public TipoDocumentoDTO tipoDocumento { get; set; }

        [Display(Order = 13)]
        [JsonPropertyName("genero")]
        [SwaggerSchema("Género del cliente.")]
        public GeneroDTO genero { get; set; }
    }
}
