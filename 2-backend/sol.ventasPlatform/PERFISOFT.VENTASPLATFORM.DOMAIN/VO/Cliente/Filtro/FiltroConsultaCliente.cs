namespace PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Cliente.Filtro
{
    public class FiltroConsultaCliente
    {
        public int[] LST_TIPODOCUMENTO { get; set; }

        public int[] LST_GENERO { get; set; }

        public string? NUMERO_DOCUMENTO { get; set; }

        public string? NOMBRES { get; set; }

        public string? APELLIDOS { get; set; }

        public string? CELULAR { get; set; }

        public string? DIRECCION { get; set; }

        public string? CORREO_ELECTRONICO { get; set; }

        public DateTime? FECHA_REGISTRO_INICIO { get; set; }

        public DateTime? FECHA_REGISTRO_FIN { get; set; }
    }
}
