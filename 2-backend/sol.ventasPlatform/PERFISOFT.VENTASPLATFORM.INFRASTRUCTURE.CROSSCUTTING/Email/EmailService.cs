using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNotificationAsync(string correoPara, string correoContenido, string asunto)
        {
            var email = new MimeMessage();

            var fromName = _configuration.GetSection("Email:Gmail:Notification:FromUser").Value;
            var fromEmail = _configuration.GetSection("Email:Gmail:Notification:From").Value;

            email.From.Add(new MailboxAddress(fromName, fromEmail));
            email.To.Add(MailboxAddress.Parse(correoPara));
            email.Subject = asunto;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = correoContenido
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration.GetSection("Email:Gmail:Host").Value,
                Convert.ToInt32(_configuration.GetSection("Email:Gmail:Port").Value),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                _configuration.GetSection("Email:Gmail:Notification:From").Value,
                _configuration.GetSection("Email:Gmail:Notification:PassWord").Value
            );

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendWithAttachmentAsync(string correoPara, string asunto, string cuerpoHtml, string nombreArchivo, byte[] archivoAdjunto)
        {
            var email = new MimeMessage();

            var fromName = _configuration.GetSection("Email:Gmail:Notification:FromUser").Value;
            var fromEmail = _configuration.GetSection("Email:Gmail:Notification:From").Value;

            email.From.Add(new MailboxAddress(fromName, fromEmail));
            email.To.Add(MailboxAddress.Parse(correoPara));
            email.Subject = asunto;

            var body = new TextPart(TextFormat.Html) { Text = cuerpoHtml };

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(new MemoryStream(archivoAdjunto)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = nombreArchivo
            };

            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            email.Body = multipart;

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration.GetSection("Email:Gmail:Host").Value,
                Convert.ToInt32(_configuration.GetSection("Email:Gmail:Port").Value),
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _configuration.GetSection("Email:Gmail:Notification:From").Value,
                _configuration.GetSection("Email:Gmail:Notification:PassWord").Value);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public string GenerateOTPRegistroUsuario()
        {
            Random random = new Random();
            string otp = random.Next(10000, 99999).ToString();
            return otp;
        }
    }
}
