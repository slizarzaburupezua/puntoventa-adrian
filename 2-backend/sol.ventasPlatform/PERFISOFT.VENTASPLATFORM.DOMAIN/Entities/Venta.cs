using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Venta : Entity
    {
        public int ID { get; set; }

        public int ID_USUARIO { get; set; }

        public int? ID_CLIENTE { get; set; }

        public string NUMERO_VENTA { get; set; }

        public DateTime FECHA_VENTA { get; set; }

        public decimal PRECIO_TOTAL { get; set; }

        public string ID_BOLETAFACTURA { get; set; }

        public string URLBOLETAFACTURA { get; set; }

        public string NOTA_ADICIONAL { get; set; }

        #region AUDITORIA

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion

        #region RELATIONSHIP

        public virtual Usuario USUARIO { get; set; }

        public virtual Cliente CLIENTE { get; set; }

        public virtual List<DetalleVenta> DETALLEVENTA { get; set; }

        #endregion

    }
}
