using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class DetalleVenta : Entity
    {
        public int ID { get; set; }

        public int ID_VENTA { get; set; }

        public int ID_PRODUCTO { get; set; }

        public string NOMBRE_PRODUCTO { get; set; }

        public string COLOR_PRODUCTO { get; set; }

        public string NOMBRE_CATEGORIA { get; set; }

        public string COLOR_CATEGORIA { get; set; }

        public string NOMBRE_MARCA { get; set; }

        public string COLOR_MARCA { get; set; }

        public int CANTIDAD { get; set; }

        public decimal PRECIO_COMPRA { get; set; }

        public decimal PRECIO_VENTA { get; set; }

        public decimal PRECIO_TOTAL { get; set; }

        public string URLFOTO_PRODUCTO { get; set; }

        #region AUDITORIA

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion

        #region RELATIONSHIP

        public virtual Venta VENTA { get; set; }

        public virtual Producto PRODUCTO { get; set; }

        #endregion

    }
}
