namespace PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON
{
    public interface IEmailService
    {
        Task SendNotificationAsync(string correoPara, string correoContenido, string asunto);

        Task SendWithAttachmentAsync(string correoPara, string asunto, string cuerpoHtml, string nombreArchivo, byte[] archivoAdjunto);

        string GenerateOTPRegistroUsuario();
    }
}
