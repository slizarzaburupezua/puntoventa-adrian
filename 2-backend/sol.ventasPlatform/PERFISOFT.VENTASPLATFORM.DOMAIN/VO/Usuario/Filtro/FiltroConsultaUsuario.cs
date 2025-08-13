namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Filtro
{
    public class FiltroConsultaUsuario
    {
        public int[] LST_GENERO { get; set; }

        public string CORREO_ELECTRONICO { get; set; }

        public string NOMBRES { get; set; }

        public string APELLIDOS { get; set; }

        public DateTime? FECHA_REGISTRO_INICIO { get; set; }

        public DateTime? FECHA_REGISTRO_FIN { get; set; }

    }
}
