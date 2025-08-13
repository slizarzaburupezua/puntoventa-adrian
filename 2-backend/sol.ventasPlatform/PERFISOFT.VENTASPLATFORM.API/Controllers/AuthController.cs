using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace PERFISOFT.VENTASPLATFORM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService
                              )
        {
            _authService = authService;
        }

        [SwaggerOperation(
        Summary = "Servicio que valida las credenciales del usuario",
        OperationId = "IniciaSesionAsync")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("iniciaSesionAsync")]
        public async Task<IActionResult> IniciaSesionAsync(IniciaSesionRequest request)
        {
            return Ok(await _authService.IniciaSesionAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que genera un enlace para que el usuario pueda restablecer su contraseña",
        OperationId = "NotifyOlvideContraseniaAsync")]
        [SwaggerResponse(200, "Token Generado para restablecer la contraseña del usuario")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("notifyOlvideContraseniaAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> NotifyOlvideContraseniaAsync([FromBody] NotifyOlvideContraseniaRequest request)
        {
            return Ok(await _authService.NotifyOlvideContraseniaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que restablecer la contraseña del usuario",
        OperationId = "RestablecerContraseniaAsync")]
        [SwaggerResponse(200, "Contraseña actualizada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("restablecerContraseniaAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> RestablecerContraseniaAsync([FromBody] RestablecerContraseniaRequest request)
        {
            return Ok(await _authService.RestablecerContraseniaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que valida el token del usuario enviado a su correo",
        OperationId = "VerifyTokenRestablecerContraseniaAsync")]
        [SwaggerResponse(200, "Token validada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("verifyTokenRestablecerContraseniaAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyTokenRestablecerContraseniaAsync([FromQuery] VerifyTokenRestablecerContraseniaRequest request)
        {
            return Ok(await _authService.VerifyTokenRestablecerContraseniaAsync(request));
        }

    }
}
