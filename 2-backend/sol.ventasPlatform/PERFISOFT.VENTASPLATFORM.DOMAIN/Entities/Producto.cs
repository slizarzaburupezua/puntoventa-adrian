using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Producto : Entity
    {
        public int ID { get; set; }

        public int ID_CATEGORIA { get; set; }

        public int ID_MARCA { get; set; }

        public string NOMBRE { get; set; }

        public string CODIGO { get; set; }

        public string DESCRIPCION { get; set; }

        public string COLOR { get; set; }

        public decimal PRECIO_COMPRA { get; set; }

        public decimal PRECIO_VENTA { get; set; }

        public int STOCK { get; set; }

        public string ID_FOTO { get; set; }

        public string URLFOTO { get; set; }

        #region AUDITORIA

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

        #endregion

        #region RELATIONSHIP

        public virtual Categoria CATEGORIA { get; set; }

        public virtual Marca MARCA { get; set; }

        public virtual List<DetalleVenta> DETALLEVENTAS { get; set; }

        #endregion

    }
}
