using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Genero : Entity
    {
        public int ID { get; set; }

        public string CODIGO { get; set; }

        public string DESCRIPCION { get; set; }

        public int ORDEN { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #region RELATIONSHIP

        public virtual List<Usuario> USUARIOS { get; set; }

        public virtual List<Cliente> CLIENTES { get; set; }

        #endregion
    }
}
