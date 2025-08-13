using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace PERFISOFT.VENTASPLATFORM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : Controller
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de ventas",
        OperationId = "GetAllByFilterAsync")]
        [SwaggerResponse(200, "lista de los clientes")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("allByFilterAsync")]
        public async Task<IActionResult> GetAllByFilterAsync([FromBody] ObtenerVentaRequest request)
        {
            return Ok(await _ventaService.GetAllByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta todos los usuarios del sistema para el filtro de busqueda",
        OperationId = "GetAllUsuariosAsync")]
        [SwaggerResponse(200, "Lista de usuarios")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allUsuariosAsync")]
        public async Task<IActionResult> GetAllUsuariosAsync()
        {
            return Ok(await _ventaService.GetAllUsuariosAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que agrega una nueva venta con su detalle",
        OperationId = "insertAsync")]
        [SwaggerResponse(200, "Venta agregada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("insertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] RegistrarVentaRequest request)
        {
            return Ok(await _ventaService.InsertAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que Anula una Venta",
        OperationId = "AnulaAsync")]
        [SwaggerResponse(200, "Vena anulada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("anulaAsync")]
        public async Task<IActionResult> AnulaAsync([FromBody] AnularVentaRequest request)
        {
            return Ok(await _ventaService.AnulaAsync(request));
        }

    }
}
