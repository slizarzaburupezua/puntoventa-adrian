using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Usuario : Entity
    {
        public int ID { get; set; }

        public int ID_TIPO_DOCUMENTO { get; set; }

        public string NUMERO_DOCUMENTO { get; set; }

        public int ID_ROL { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public int ID_GENERO { get; set; }

        public string CORREO_ELECTRONICO { get; set; }

        public string? CELULAR { get; set; }

        public string? DIRECCION { get; set; }

        public DateTime FECHA_NACIMIENTO { get; set; }

        public bool FLG_CAMBIAR_CLAVE { get; set; }

        public bool FLG_PRIMERA_VEZ_LOGUEO { get; set; }

        public DateTime? FECHA_ULTIMO_ACCESO { get; set; }

        public string? ID_FOTO { get; set; }

        public string? URLFOTO { get; set; }

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #region RELATIONSHIP

        public virtual TipoDocumento TIPODOCUMENTO { get; set; }

        public virtual UsuarioId USUARIOID { get; set; }

        public virtual UsuarioContrasena USUARIOCONTRASENA { get; set; }

        public virtual Genero GENERO { get; set; }

        public virtual Rol ROL { get; set; }

        public virtual List<Venta> VENTAS { get; set; }

        #endregion

    }
}
