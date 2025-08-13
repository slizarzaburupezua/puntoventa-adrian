using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response
{
    public class UsuarioDTO
    {
        [Display(Order = 1)]
        [JsonPropertyName("nombres")]
        [SwaggerSchema("Nombres del Usuario.")]
        public string Nombres { get; set; }

        [Display(Order = 2)]
        [JsonPropertyName("apellidos")]
        [SwaggerSchema("Apellidos del Usuario.")]
        public string Apellidos { get; set; }

        [Display(Order = 3)]
        [JsonPropertyName("idGenero")]
        [SwaggerSchema("Identificador del género del Usuario.")]
        public int Id_Genero { get; set; }

        [Display(Order = 4)]
        [JsonPropertyName("idEstadoCuenta")]
        [SwaggerSchema("Identificador del estado de la cuenta.")]
        public int Id_Estado_Cuenta { get; set; }

        [Display(Order = 5)]
        [JsonPropertyName("idTipoDocumento")]
        [SwaggerSchema("Identificador del tipo de documento.")]
        public int Id_Tipo_Documento { get; set; }

        [Display(Order = 6)]
        [JsonPropertyName("numeroDocumento")]
        [SwaggerSchema("Número de documento")]
        public string Numero_Documento { get; set; }

        [Display(Order = 7)]
        [JsonPropertyName("correoElectronico")]
        [SwaggerSchema("Correo Electrónico del Usuario.")]
        public string Correo_Electronico { get; set; }

        [Display(Order = 8)]
        [JsonPropertyName("celular")]
        [SwaggerSchema("Celular del Usuario.")]
        public string Celular { get; set; }

        [Display(Order = 9)]
        [JsonPropertyName("direccion")]
        [SwaggerSchema("Dirección del Usuario.")]
        public string Direccion { get; set; }

        [Display(Order = 10)]
        [JsonPropertyName("fechaNacimiento")]
        [SwaggerSchema("Fecha Nacimiento del Usuario")]
        public DateTime Fecha_Nacimiento { get; set; }

        [Display(Order = 11)]
        [JsonPropertyName("flgCambiarClave")]
        [SwaggerSchema("Flag Recibir Boletines")]
        public bool FlgCambiarClave { get; set; }

        [Display(Order = 12)]
        [JsonPropertyName("fechaRegistro")]
        [SwaggerSchema("Fecha en la que el usuario se registró en el sistema")]
        public DateTime Fecha_Registro { get; set; }

        [Display(Order = 13)]
        [JsonPropertyName("fechaActualizacion")]
        [SwaggerSchema("Fecha en la que el usuario se registró en el sistema")]
        public DateTime? Fecha_Actualizacion { get; set; }

        [Display(Order = 14)]
        [JsonPropertyName("activo")]
        [SwaggerSchema("Flag que indica si el producto se encuentra activo.")]
        public bool Activo { get; set; }

        [Display(Order = 15)]
        [JsonPropertyName("usuarioID")]
        [SwaggerSchema("Objeto que contiene el identificador Guid del usuario")]
        public UsuarioIdDTO UsuarioId { get; set; }

        [Display(Order = 16)]
        [JsonPropertyName("genero")]
        [SwaggerSchema("Genero")]
        public GeneroDTO Genero { get; set; }

        [Display(Order = 17)]
        [JsonPropertyName("tipoDocumento")]
        [SwaggerSchema("Tipo de Documento")]
        public TipoDocumentoDTO TipoDocumento { get; set; }

        [Display(Order = 18)]
        [JsonPropertyName("rol")]
        [SwaggerSchema("Rol")]
        public RolDTO Rol { get; set; }

        [Display(Order = 19)]
        [JsonPropertyName("urlFoto")]
        [SwaggerSchema("Url de la foto")]
        public string UrlFoto { get; set; }
    }
}
