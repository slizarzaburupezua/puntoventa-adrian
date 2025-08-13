using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class UsuarioId : Entity
    {
        public int ID { get; set; }

        public int ID_USUARIO { get; set; }

        public Guid ID_USUARIO_GUID { get; set; }

        #region AUDITORIA

        public bool ESTADO { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion

        #region RELATIONSHIP

        public virtual Usuario USUARIO { get; set; }

        #endregion
    }
}
