namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Menu.Consulta
{
    public class ObtenerMenuRol
    {
        public string PADRE_TEXTO { get; set; }

        public string HIJO_TEXTO { get; set; }

        public string TITULO { get; set; }

        public string TIPO { get; set; }

        public string ICONO { get; set; }

        public bool FLG_ENLACE_EXTERNO { get; set; }

        public bool FLG_MENU_HIJO { get; set; }

        public string RUTA { get; set; }

        public int ORDEN { get; set; }

    }
}
