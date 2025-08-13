using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Parametro : Entity
    {
        public int ID { get; set; }

        public string NOMBRE { get; set; }

        public string DESCRIPCION { get; set; }

        #region AUDITORIA

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion
    }
}
