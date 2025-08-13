using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace PERFISOFT.VENTASPLATFORM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService
                              )
        {
            _usuarioService = usuarioService;
        }

        [SwaggerOperation(
        Summary = "Servicio que valida si existe el cowrreo electronico",
        OperationId = "ExistCorreoAsync")]
        [SwaggerResponse(200, "Respuesta si existe")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("existCorreoAsync")]
        public async Task<IActionResult> ExistCorreoAsync([FromBody] ExistCorreoUsuarioRequest request)
        {
            return Ok(await _usuarioService.ExistCorreoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que valida si existe el número de documento del usuario",
        OperationId = "ExistNumeroDocumentoAsync")]
        [SwaggerResponse(200, "Respuesta si existe")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("existNumeroDocumentoAsync")]
        public async Task<IActionResult> ExistNumeroDocumentoAsync([FromBody] ExistNumeroDocumentoUsuarioRequest request)
        {
            return Ok(await _usuarioService.ExistNumeroDocumentoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta el detalle de la información del usuario",
        OperationId = "GetPersonalInfoAsync")]
        [SwaggerResponse(200, "Información del usuario")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("personalInfoAsync")]
        public async Task<IActionResult> GetPersonalInfoAsync(Guid idUsuario)
        {
            return Ok(await _usuarioService.GetPersonalInfoAsync(idUsuario));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de los usuarios",
        OperationId = "GetAllUsersByFilterAsync")]
        [SwaggerResponse(200, "lista de los usuarios")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("allByFilterAsync")]
        public async Task<IActionResult> GetAllByFilterAsync([FromBody] ObtenerUsuarioRequest request)
        {
            return Ok(await _usuarioService.GetAllByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta todos los usuarios activos",
        OperationId = "GetAllActivesAsync")]
        [SwaggerResponse(200, "lista de los usuarios activos")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("allActivesAsync")]
        public async Task<IActionResult> GetAllActivesAsync([FromBody] ObtenerColaboradoresRequest request)
        {
            return Ok(await _usuarioService.GetAllActivesAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que registra un nuevo usuario",
        OperationId = "InsertAsync")]
        [SwaggerResponse(200, "Nuevo usuario registrado")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [AllowAnonymous]
        [HttpPost("insertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] RegistrarUsuarioRequest request)
        {
            return Ok(await _usuarioService.InsertAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza un Usuario",
        OperationId = "UpdateAsync")]
        [SwaggerResponse(200, "Usuario actualizado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] ActualizarUsuarioRequest request)
        {
            return Ok(await _usuarioService.UpdateAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza si el usuario va a estar activo o inactivo",
        OperationId = "UpdateActivoAsync")]
        [SwaggerResponse(200, "Usuario actualizado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateActivoAsync")]
        public async Task<IActionResult> UpdateActivoAsync([FromBody] ActualizarActivoUsuarioRequest request)
        {
            return Ok(await _usuarioService.UpdateActivoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza la contrasenia del usuario",
        OperationId = "UpdateUsuarioContraseniaByIdAsync")]
        [SwaggerResponse(200, "Respuesta de la modificación de la información del usuario")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateUsuarioContraseniaByIdAsync")]
        public async Task<IActionResult> UpdateUsuarioContraseniaByIdAsync([FromBody] ActualizarContraseniaRequest request)
        {
            return Ok(await _usuarioService.UpdateUsuarioContraseniaByIdAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que elimina un usuario",
        OperationId = "DeleteAsync")]
        [SwaggerResponse(200, "Usuario eliminado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("deleteAsync")]
        public async Task<IActionResult> DeleteAsync([FromBody] EliminarUsuarioRequest request)
        {
            return Ok(await _usuarioService.DeleteAsync(request));
        }


        [AllowAnonymous]
        [SwaggerOperation(
        Summary = "Servicio que envia el enlace de pago al correo del usuario",
        OperationId = "EnviarEnlacePagoAsync")]
        [SwaggerResponse(200, "Enlace enviado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("enviarEnlacePagoAsync")]
        public async Task<IActionResult> EnviarEnlacePagoAsync([FromBody] EnviarEnlacePagoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DestinationTimeZoneId))
                return BadRequest();


            if (string.IsNullOrWhiteSpace(request.Correo))
                return BadRequest();

            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest();


            return Ok(await _usuarioService.EnviarEnlacePagoAsync(request));
        }

    }
}
