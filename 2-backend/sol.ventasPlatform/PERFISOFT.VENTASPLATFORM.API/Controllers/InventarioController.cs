using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace PERFISOFT.VENTASPLATFORM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventarioController : Controller
    {
        private readonly IInventarioService _inventarioService;

        public InventarioController(IInventarioService inventarioService
                      )
        {
            _inventarioService = inventarioService;
        }

        #region MEDIDA

        [SwaggerOperation(
        Summary = "Servicio que obtiene las medidas",
        OperationId = "GetAllMedidaAsync")]
        [SwaggerResponse(200, "Lista de medidas")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allMedidaAsync")]
        public async Task<IActionResult> GetAllMedidaAsync(Guid idUsuarioGuid)
        {
            return Ok(await _inventarioService.GetAllMedidaAsync(idUsuarioGuid));
        }

        #endregion

        #region CATEGORÍA

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de las categorias",
        OperationId = "GetAllCategoriaByFilterAsync")]
        [SwaggerResponse(200, "lista de las categorias")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allCategoriaByFilterAsync")]
        public async Task<IActionResult> GetAllCategoriaByFilterAsync([FromQuery] ObtenerCategoriaRequest request)
        {
            return Ok(await _inventarioService.GetAllCategoriaByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de las categorías para el comboBox",
        OperationId = "GetAllCategoriasForComboBoxAsync")]
        [SwaggerResponse(200, "lista de las categorías para el comboBox")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allCategoriasForComboBoxAsync")]
        public async Task<IActionResult> GetAllCategoriasForComboBoxAsync()
        {
            return Ok(await _inventarioService.GetAllCategoriasForComboBoxAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que agrega una categoria",
        OperationId = "InsertCategoriaAsync")]
        [SwaggerResponse(200, "Categoría agregada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("insertCategoriaAsync")]
        public async Task<IActionResult> InsertCategoriaAsync([FromBody] RegistrarCategoriaRequest request)
        {
            return Ok(await _inventarioService.InsertCategoriaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza una categoria",
        OperationId = "UpdateCategoriaAsync")]
        [SwaggerResponse(200, "Categoría actualizada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateCategoriaAsync")]
        public async Task<IActionResult> UpdateCategoriaAsync([FromBody] ActualizarCategoriaRequest request)
        {
            return Ok(await _inventarioService.UpdateCategoriaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza si la categoría va a estar activo o inactivo",
        OperationId = "UpdateActivoCategoriaAsync")]
        [SwaggerResponse(200, "Categoría actualizada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateActivoCategoriaAsync")]
        public async Task<IActionResult> UpdateActivoCategoriaAsync([FromBody] ActualizarActivoCategoriaRequest request)
        {
            return Ok(await _inventarioService.UpdateActivoCategoriaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que elimina una categoria",
        OperationId = "DeleteCategoriaAsync")]
        [SwaggerResponse(200, "Categoría eliminada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("deleteCategoriaAsync")]
        public async Task<IActionResult> DeleteCategoriaAsync([FromBody] EliminarCategoriaRequest request)
        {
            return Ok(await _inventarioService.DeleteCategoriaAsync(request));
        }

        #endregion

        #region PRODUCTO

        [SwaggerOperation(
        Summary = "Servicio que consulta el producto por código del producto",
        OperationId = "GetProductoByCodeAsync")]
        [SwaggerResponse(200, "consulta de producto")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("productosByCodeAsync")]
        public async Task<IActionResult> GetProductoByCodeAsync([FromQuery] ObtenerProductoPorCodigoRequest request)
        {
            return Ok(await _inventarioService.GetProductoByCodeAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta las categorias de los productos con la cantidad total de productos que tienen esas categorías",
        OperationId = "GetCategoriesWithProductsCountAsync")]
        [SwaggerResponse(200, "consulta de categorías")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("categoriesWithProductsCountAsync")]
        public async Task<IActionResult> GetCategoriesWithProductsCountAsync()
        {
            return Ok(await _inventarioService.GetCategoriesWithProductsCountAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta los productos asociadas a una categoría",
        OperationId = "GetAllProductsByCategoryAsync")]
        [SwaggerResponse(200, "consulta de categorías")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allProductsByCategoryAsync")]
        public async Task<IActionResult> GetAllProductsByCategoryAsync([FromQuery] int idCategory)
        {
            return Ok(await _inventarioService.GetAllProductsByCategoryAsync(idCategory));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de los productos",
        OperationId = "GetAllProductoByFilterAsync")]
        [SwaggerResponse(200, "lista de los productos")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("allProductoByFilterAsync")]
        public async Task<IActionResult> GetAllProductoByFilterAsync([FromBody] ObtenerProductoRequest request)
        {
            return Ok(await _inventarioService.GetAllProductoByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de los productos para el comboBox",
        OperationId = "GetAllProductoForComboBoxAsync")]
        [SwaggerResponse(200, "lista de los productos para el comboBox")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allProductosForComboBoxAsync")]
        public async Task<IActionResult> GetAllProductoForComboBoxAsync()
        {
            return Ok(await _inventarioService.GetAllProductoForComboBoxAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que agrega un nuevo producto",
        OperationId = "InsertProductoAsync")]
        [SwaggerResponse(200, "Producto agregada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("insertProductoAsync")]
        public async Task<IActionResult> InsertProductoAsync([FromBody] RegistrarProductoRequest request)
        {
            return Ok(await _inventarioService.InsertProductoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza un producto",
        OperationId = "UpdateProductoAsync")]
        [SwaggerResponse(200, "Producto actualizado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateProductoAsync")]
        public async Task<IActionResult> UpdateProductoAsync([FromBody] ActualizarProductoRequest request)
        {
            return Ok(await _inventarioService.UpdateProductoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza si el producto va a estar activo o inactivo",
        OperationId = "UpdateActivoProductoAsync")]
        [SwaggerResponse(200, "Producto actualizada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateActivoProductoAsync")]
        public async Task<IActionResult> UpdateActivoProductoAsync([FromBody] ActualizarActivoProductoRequest request)
        {
            return Ok(await _inventarioService.UpdateActivoProductoAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que elimina un producto",
        OperationId = "DeleteProductoAsync")]
        [SwaggerResponse(200, "Producto eliminado correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("deleteProductoAsync")]
        public async Task<IActionResult> DeleteProductoAsync([FromBody] EliminarProductoRequest request)
        {
            return Ok(await _inventarioService.DeleteProductoAsync(request));
        }

        #endregion

        #region MARCA

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de las marcas",
        OperationId = "GetAllMarcaByFilterAsync")]
        [SwaggerResponse(200, "lista de las categorias")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allMarcaByFilterAsync")]
        public async Task<IActionResult> GetAllMarcaByFilterAsync([FromQuery] ObtenerMarcaRequest request)
        {
            return Ok(await _inventarioService.GetAllMarcaByFilterAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que consulta la lista de las marcas para el comboBox",
        OperationId = "GetAllMarcasForComboBoxAsync")]
        [SwaggerResponse(200, "lista de las marcas para el comboBox")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpGet("allMarcasForComboBoxAsync")]
        public async Task<IActionResult> GetAllMarcasForComboBoxAsync()
        {
            return Ok(await _inventarioService.GetAllMarcasForComboBoxAsync());
        }

        [SwaggerOperation(
        Summary = "Servicio que agrega una marca",
        OperationId = "InsertMarcaAsync")]
        [SwaggerResponse(200, "Marca agregada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("insertMarcaAsync")]
        public async Task<IActionResult> InsertMarcaAsync([FromBody] RegistrarMarcaRequest request)
        {
            return Ok(await _inventarioService.InsertMarcaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza una marca",
        OperationId = "UpdateMarcaAsync")]
        [SwaggerResponse(200, "Marca actualizada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateMarcaAsync")]
        public async Task<IActionResult> UpdateMarcaAsync([FromBody] ActualizarMarcaRequest request)
        {
            return Ok(await _inventarioService.UpdateMarcaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que actualiza si la marca va a estar activo o inactivo",
        OperationId = "UpdateActivoMarcaAsync")]
        [SwaggerResponse(200, "Marca actualizada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("updateActivoMarcaAsync")]
        public async Task<IActionResult> UpdateActivoMarcaAsync([FromBody] ActualizarActivoMarcaRequest request)
        {
            return Ok(await _inventarioService.UpdateActivoMarcaAsync(request));
        }

        [SwaggerOperation(
        Summary = "Servicio que elimina una marca",
        OperationId = "DeleteMarcaAsync")]
        [SwaggerResponse(200, "Marca eliminada correctamente")]
        [SwaggerResponse(500, "Error interno en el servidor")]
        [HttpPost("deleteMarcaAsync")]
        public async Task<IActionResult> DeleteMarcaAsync([FromBody] EliminarMarcaRequest request)
        {
            return Ok(await _inventarioService.DeleteMarcaAsync(request));
        }

        #endregion
    }
}
