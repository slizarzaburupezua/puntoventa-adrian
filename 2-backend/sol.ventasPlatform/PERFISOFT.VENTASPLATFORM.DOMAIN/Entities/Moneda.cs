using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Moneda : Entity
    {
        public int ID { get; set; }

        public string REGION_ISO_DOS_LETRAS { get; set; }

        public string REGION_ISO_TRES_LETRAS { get; set; }

        public string CODIGO_MONEDA { get; set; }

        public string LENGUAJE_CODIGO { get; set; }

        public string LENGUAJE_DESCRIPCION { get; set; }

        public string CULTUREINFO { get; set; }

        public string PAIS { get; set; }

        public string DESCRIPCION { get; set; }

        public string SIMBOLO { get; set; }

        public int ORDEN { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #region RELATIONSHIP

        public virtual Negocio NEGOCIO { get; set; }

        #endregion

    }
}
