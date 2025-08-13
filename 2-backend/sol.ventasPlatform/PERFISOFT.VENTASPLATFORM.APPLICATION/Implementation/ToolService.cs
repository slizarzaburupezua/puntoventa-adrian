using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Tool.Email;
using Serilog;
using System.Security.Claims;
using DR = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class ToolService : IToolService
    {
        private readonly IEmailService _emailService;

        public ToolService(IEmailService emailService,
                           IConfiguration configuration,
                           IUsuarioRepository usuarioRepository
                            )
        {
            _emailService = emailService;

        }

        public async Task<ResponseDTO> SendEmailAsync(EnviarCorreoRequest request)
        {
            await _emailService.SendNotificationAsync(request.CorreoPara, request.CorreoContenido, "");

            return new ResponseDTO
            {
                Success = Flags.SuccessTransaction,
                Message = string.Format(DR.Dictionary.SuccessEmailTransaction, request.CorreoPara)
            };
        }

        public Task<bool> IsUserAuthorizedAsync(Guid requestId, HttpContext context)
        {
            if (context.User?.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
            {
                LogUnauthorizedAccess();
                return Task.FromResult(false);
            }

            var idUsuarioClaim = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Task.FromResult(string.Equals(idUsuarioClaim, requestId.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        public bool EsBase64(string cadena)
        {
            if (string.IsNullOrEmpty(cadena)) return false;

            var regex = new System.Text.RegularExpressions.Regex(@"^([A-Za-z0-9+/=]|\n|\r)*$");
            if (!regex.IsMatch(cadena)) return false;

            if (cadena.Length % 4 != 0) return false;

            try
            {
                Convert.FromBase64String(cadena);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ResponseDTO CreateResponse(string code, bool Success, string message, string value)
        {
            return new ResponseDTO
            {
                Code = code,
                Success = Success,
                Message = message,
                Value = value
            };
        }

        public string ConvertCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains("@"))
                return "Correo inválido";

            var partesCorreo = correo.Split('@');
            var nombreUsuario = partesCorreo[0];
            var dominio = partesCorreo[1];

            if (nombreUsuario.Length < 3)
                return "Nombre de usuario del correo muy corto";

            var primerosCaracteres = nombreUsuario.Substring(0, 2);
            var ultimosCaracteres = nombreUsuario.Substring(nombreUsuario.Length - 2, 2);

            var nombreOculto = $"{primerosCaracteres}***{ultimosCaracteres}";

            return $"{nombreOculto}@{dominio}";
        }

        private void LogUnauthorizedAccess()
        {
            _ = Task.Run(() => Log.Error(LogMessages.UsuarioNoAutorizado));
        }

    }
}
