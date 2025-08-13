using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace PERFISOFT.VENTASPLATFORM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClienteController : Controller
    {   
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService
                      )
        {
            _clienteService = clienteService;
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de los clientes",
        OperationId = "GetAllByFilterAsync")]
        [SwaggerResponse(200, "lista de los clientes")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("allByFilterAsync")]
        public async Task<IActionResult> GetAllByFilterAsync([FromBody] ObtenerClienteRequest request)
        {
            return Ok(await _clienteService.GetAllByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que valida si existe el cowrreo electronico",
        OperationId = "ExistCorreoAsync")]
        [SwaggerResponse(200, "Respuesta si existe")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("existCorreoAsync")]
        public async Task<IActionResult> ExistCorreoAsync([FromBody] ExistCorreoClienteRequest request)
        {
            return Ok(await _clienteService.ExistCorreoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que valida si existe el número de documento del cliente",
        OperationId = "ExistNumeroDocumentoAsync")]
        [SwaggerResponse(200, "Respuesta si existe")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("existNumeroDocumentoAsync")]
        public async Task<IActionResult> ExistNumeroDocumentoAsync([FromBody] ExistNumeroDocumentoClienteRequest request)
        {
            return Ok(await _clienteService.ExistNumeroDocumentoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta el cliente por número de documento o por correo",
        OperationId = "GetByNumDocumentoCorreoAsync")]
        [SwaggerResponse(200, "consulta del cliente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("byNumDocumentoCorreoAsync")]
        public async Task<IActionResult> GetByNumDocumentoCorreoAsync([FromQuery] ObtenerClientePorNumDocumentoCorreoRequest request)
        {
            return Ok(await _clienteService.GetByNumDocumentoCorreoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que agrega un nuevo cliente",
        OperationId = "InsertAsync")]
        [SwaggerResponse(200, "Cliente agregado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("insertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] RegistrarClienteRequest request)
        {
            return Ok(await _clienteService.InsertAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza un cliente",
        OperationId = "UpdateAsync")]
        [SwaggerResponse(200, "Cliente actualizado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] ActualizarClienteRequest request)
        {
            return Ok(await _clienteService.UpdateAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza si el cliente va a estar activo o inactivo",
        OperationId = "UpdateActivoClienteAsync")]
        [SwaggerResponse(200, "Cliente actualizado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateActivoAsync")]
        public async Task<IActionResult> UpdateActivoAsync([FromBody] ActualizarActivoClienteRequest request)
        {
            return Ok(await _clienteService.UpdateActivoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que elimina un cliente",
        OperationId = "DeleteAsync")]
        [SwaggerResponse(200, "Cliente eliminado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("deleteAsync")]
        public async Task<IActionResult> DeleteAsync([FromBody] EliminarClienteRequest request)
        {
            return Ok(await _clienteService.DeleteAsync(request));
        }
    }
}
