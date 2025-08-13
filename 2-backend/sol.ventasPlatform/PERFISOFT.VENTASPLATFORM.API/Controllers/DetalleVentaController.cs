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

    public class DetalleVentaController : Controller
    {
        private readonly IDetalleVentaService _detalleVentaService;

        public DetalleVentaController(IDetalleVentaService detalleVentaService)
        {
            _detalleVentaService = detalleVentaService;
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta el detalle de la venta por identificador",
        OperationId = "GetDetalleAsync")]
        [SwaggerResponse(200, "Detalle de la venta")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("detalleAsync")]
        public async Task<IActionResult> GetDetalleAsync([FromBody] ObtenerDetalleVentaRequest request)
        {
            return Ok(await _detalleVentaService.GetDetalleAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta detalle de venta para el análisis de graficos",
        OperationId = "GetAnalisisProductosByFilterAsync")]
        [SwaggerResponse(200, "Ventas para el análisis de graficos en base a los filtros ingresados")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("analisisProductosByFilterAsync")]
        public async Task<IActionResult> GetAnalisisProductosByFilterAsync([FromBody] ObtenerReporteProductoRequest request)
        {
            return Ok(await _detalleVentaService.GetAnalisisProductosByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta detalle de venta para el análisis de graficos",
        OperationId = "GetAnalisisCategoriasByFilterAsync")]
        [SwaggerResponse(200, "Ventas para el análisis de graficos en base a los filtros ingresados")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("analisisCategoriasByFilterAsync")]
        public async Task<IActionResult> GetAnalisisCategoriasByFilterAsync([FromBody] ObtenerReporteCategoriaRequest request)
        {
            return Ok(await _detalleVentaService.GetAnalisisCategoriasByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta detalle de venta para el análisis de graficos",
        OperationId = "GetAnalisisMarcasByFilterAsync")]
        [SwaggerResponse(200, "Ventas para el análisis de graficos en base a los filtros ingresados")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("analisisMarcasByFilterAsync")]
        public async Task<IActionResult> GetAnalisisMarcasByFilterAsync([FromBody] ObtenerReporteMarcaRequest request)
        {
            return Ok(await _detalleVentaService.GetAnalisisMarcasByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta el totalizado de ventas por fechas",
        OperationId = "GetResumenReporteAsync")]
        [SwaggerResponse(200, "Totalizado de ventas por fecha")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("resumenReporteAsync")]
        public async Task<IActionResult> GetResumenReporteAsync([FromBody] ObtenerResumenReporteRequest request)
        {
            return Ok(await _detalleVentaService.GetResumenReporteAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que genera el reporte por categorías",
        OperationId = "GetReportePorCategoriasAsync")]
        [SwaggerResponse(200, "Reporte analisis de ventas por Categoría")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("reportePorCategoriasAsync")]
        public async Task<IActionResult> GetReportePorCategoriasAsync([FromBody] ObtenerReporteCategoriaRequest request)
        {
            var response = await _detalleVentaService.GetReportePorCategoriasAsync(request);
            return File(response.ArrayValue, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte.xlsx");
        }

        [SwaggerOperation(
        Summary = "Servicio que genera el reporte por marcas",
        OperationId = "GetReportePorMarcasAsync")]
        [SwaggerResponse(200, "Reporte analisis de ventas por Marca")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("reportePorMarcasAsync")]
        public async Task<IActionResult> GetReportePorMarcasAsync([FromBody] ObtenerReporteMarcaRequest request)
        {
            var response = await _detalleVentaService.GetReportePorMarcasAsync(request);
            return File(response.ArrayValue, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte.xlsx");
        }

        [SwaggerOperation(
        Summary = "Servicio que genera el reporte por productos",
        OperationId = "GetReportePorProductosAsync")]
        [SwaggerResponse(200, "Reporte analisis de ventas por Producto")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("reportePorProductosAsync")]
        public async Task<IActionResult> GetReportePorProductosAsync([FromBody] ObtenerReporteProductoRequest request)
        {
            var response = await _detalleVentaService.GetReportePorProductosAsync(request);
            return File(response.ArrayValue, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte.xlsx");
        }

    }
}
