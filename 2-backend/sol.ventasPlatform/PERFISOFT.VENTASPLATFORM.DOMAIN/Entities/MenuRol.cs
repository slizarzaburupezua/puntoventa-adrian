namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class MenuRol
    {
        public int ID { get; set; }

        public int ID_MENU { get; set; }

        public int ID_ROL { get; set; }

        public bool ACTIVO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }
    }
}
