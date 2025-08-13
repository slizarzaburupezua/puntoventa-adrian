using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace PERFISOFT.VENTASPLATFORM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParametrosGeneralesController : Controller
    {
        private readonly IParametrosGeneralesService _parametroService;

        public ParametrosGeneralesController(IParametrosGeneralesService parametroService)
        {
            _parametroService = parametroService;
        }

        [SwaggerOperation(
        Summary = "Servicio que obtiene los roles del sistema",
        OperationId = "GetAllRolAsync")]
        [SwaggerResponse(200, "Lista de roles")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allRolAsync")]
        public async Task<IActionResult> GetAllRolAsync()
        {
            return Ok(await _parametroService.GetAllRolAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que obtiene la moneda",
        OperationId = "GetAllMonedaAsync")]
        [SwaggerResponse(200, "Lista de monedas")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allMonedaAsync")]
        public async Task<IActionResult> GetAllMonedaAsync()
        {
            return Ok(await _parametroService.GetAllMonedaAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que obtiene el tipo de documento",
        OperationId = "GetAllTipoDocumentoAsync")]
        [SwaggerResponse(200, "Lista de tipo de documento")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allTipoDocumentoAsync")]
        public async Task<IActionResult> GetAllTipoDocumentoAsync()
        {
            return Ok(await _parametroService.GetAllTipoDocumentoAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que obtiene el genero",
        OperationId = "GetAllGeneroAsync")]
        [SwaggerResponse(200, "Lista de genero")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allGeneroAsync")]
        public async Task<IActionResult> GetAllGeneroAsync()
        {
            return Ok(await _parametroService.GetAllGeneroAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que obtiene la información el negocio",
        OperationId = "GetNegocioAsync")]
        [SwaggerResponse(200, "Información del negocio")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("negocioAsync")]
        public async Task<IActionResult> GetNegocioAsync()
        {
            return Ok(await _parametroService.GetNegocioAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza la información de la empresa",
        OperationId = "UpdateNegocioAsync")]
        [SwaggerResponse(200, "Información de la empresa actualizada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateNegocioAsync")]
        public async Task<IActionResult> UpdateNegocioAsync([FromBody] ActualizarNegocioRequest request)
        {
            return Ok(await _parametroService.UpdateNegocioAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que obtiene la vista previa de la boleta o factura",
        OperationId = "VistaPreviaBoletaFacturaAsync")]
        [SwaggerResponse(200, "Vista previa de la boleta factura generada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("vistaPreviaBoletaFacturaAsync")]
        public async Task<IActionResult> VistaPreviaBoletaFacturaAsync([FromBody] ActualizarNegocioRequest request)
        {
            return Ok(await _parametroService.VistaPreviaBoletaFacturaAsync(request));
        }
    }
}
