namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Tool.Email
{
    public class EnviarCorreoRequest
    {
        public int IdUsuario { get; set; }

        public string CorreoPara { get; set; }

        public string CorreoContenido { get; set; }
    }
}
