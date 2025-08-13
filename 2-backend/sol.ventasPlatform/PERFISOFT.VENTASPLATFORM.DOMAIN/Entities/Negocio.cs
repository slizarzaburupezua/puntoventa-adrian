using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Negocio : Entity
    {
        public int ID { get; set; }

        public int ID_MONEDA { get; set; }

        public string RAZON_SOCIAL { get; set; }

        public string RUC { get; set; }

        public string DIRECCION { get; set; }

        public string CELULAR { get; set; }

        public string CORREO_ELECTRONICO { get; set; }

        public string ID_FOTO { get; set; }

        public string URLFOTO { get; set; }

        public string COLOR_BOLETA_FACTURA { get; set; }

        public string FORMATO_IMPRESION { get; set; }

        #region AUDITORIA

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion

        #region RELATIONSHIP

        public virtual Moneda MONEDA { get; set; }

        #endregion
    }
}
