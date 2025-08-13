using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Entities
{
    public class Menu : Entity
    {
        public int ID { get; set; }

        public string PADRE_TEXTO { get; set; }

        public string HIJO_TEXTO { get; set; }

        public string TITULO { get; set; }

        public string TIPO { get; set; }

        public string ICONO { get; set; }

        public bool FLG_ENLACE_EXTERNO { get; set; }

        public bool FLG_MENU_HIJO { get; set; }

        public string RUTA { get; set; }

        public int ORDEN { get; set; }

        public bool ACTIVO { get; set; }

        public bool ESTADO { get; set; }

        public string? MOTIVO_ANULACION { get; set; }

        public DateTime FECHA_REGISTRO { get; set; }

        public DateTime? FECHA_ACTUALIZACION { get; set; }

        public DateTime? FECHA_ANULACION { get; set; }

    }
}
