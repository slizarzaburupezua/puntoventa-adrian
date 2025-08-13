using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace PERFISOFT.VENTASPLATFORM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroController : Controller
    {
        private readonly IParametroService _parametroService;

        public ParametroController(IParametroService parametroService
                      )
        {
            _parametroService = parametroService;
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta un valor de la tabla parametro detalle basado en el subkey",
        OperationId = "GetValueBySubKeyAsync")]
        [SwaggerResponse(200, "Valor del Subkey")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("valueBySubKeyAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> GetValueBySubKeyAsync([FromQuery] string key, [FromQuery] string subKey)
        {
            return Ok(await _parametroService.GetValueBySubKeyAsync(key, subKey));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de los parámetros disponibles",
        OperationId = "GetAllAsync")]
        [SwaggerResponse(200, "lista de los parámetros")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allAsync")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _parametroService.GetAllAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta el detalle de un Parámetro por Identificador",
        OperationId = "GetAllDetalleByIdAsync")]
        [SwaggerResponse(200, "lista del detalle del Parámetro")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allDetalleByIdAsync")]
        [Authorize]
        public async Task<IActionResult> GetAllDetalleByIdAsync([FromQuery] int idParametro, [FromQuery] Guid idUsuarioGuid)
        {
            return Ok(await _parametroService.GetAllDetalleByIdAsync(idParametro, idUsuarioGuid));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta el detalle de un Parámetro por Key",
        OperationId = "GetAllDetalleByKeyAsync")]
        [SwaggerResponse(200, "lista del detalle del Parámetro")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allDetalleByKeyAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDetalleByKeyAsync([FromQuery] string keyParam)
        {
            return Ok(await _parametroService.GetAllDetalleByKeyAsync(keyParam));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza un deltalle de un parámetro",
        OperationId = "UpdateDetalleParametroAsync")]
        [SwaggerResponse(200, "Detalle del producto actualizado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateDetalleParametroAsync")]
        [Authorize]
        public async Task<IActionResult> UpdateDetalleParametroAsync([FromBody] ActualizarParametroDetalleRequest request)
        {
            return Ok(await _parametroService.UpdateDetalleParametroAsync(request));
        }
    }
}
