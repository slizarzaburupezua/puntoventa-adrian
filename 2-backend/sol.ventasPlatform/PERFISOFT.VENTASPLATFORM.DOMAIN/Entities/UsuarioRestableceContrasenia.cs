using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class UsuarioRestableceContrasenia : Entity
    {
        public int ID { get; set; }

        public string CORREO_ELECTRONICO { get; set; }

        public string TOKEN { get; set; }

        public bool VERIFICADO { get; set; }

        #region AUDITORIA

        public bool ESTADO { get; set; }

        public string MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion
    }
}
