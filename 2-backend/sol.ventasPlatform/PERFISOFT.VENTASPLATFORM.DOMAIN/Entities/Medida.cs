using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Medida : Entity
    {
        public int ID { get; set; }

        public string NOMBRE { get; set; }

        public string? DESCRIPCION { get; set; }

        public string ABREVIATURA { get; set; }

        public string EQUIVALENTE { get; set; }

        public int VALOR { get; set; }

        #region AUDITORIA

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion

        #region RELATIONSHIP

        public virtual List<Categoria> CATEGORIAS { get; set; }

        #endregion

    }
}
