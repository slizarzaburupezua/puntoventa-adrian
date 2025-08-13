using Microsoft.AspNetCore.Http;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Tool.Email;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IToolService
    {
        Task<ResponseDTO> SendEmailAsync(EnviarCorreoRequest request);

        ResponseDTO CreateResponse(string code, bool Success, string message, string value);

        Task<bool> IsUserAuthorizedAsync(Guid requestId, HttpContext context);

        bool EsBase64(string cadena);

        string ConvertCorreo(string correo);

    }
}
