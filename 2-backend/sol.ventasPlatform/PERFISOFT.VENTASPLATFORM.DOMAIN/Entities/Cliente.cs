using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Cliente : Entity
    {
        public int ID { get; set; }

        public int ID_TIPO_DOCUMENTO { get; set; }

        public string NUMERO_DOCUMENTO { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public string? CORREO_ELECTRONICO { get; set; }

        public int ID_GENERO { get; set; }

        public string? CELULAR { get; set; }

        public string? DIRECCION { get; set; }

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #region RELATIONSHIP

        public virtual TipoDocumento TIPODOCUMENTO { get; set; }

        public virtual Genero GENERO { get; set; }

        public virtual List<Venta> VENTAS { get; set; }

        #endregion

    }
}
