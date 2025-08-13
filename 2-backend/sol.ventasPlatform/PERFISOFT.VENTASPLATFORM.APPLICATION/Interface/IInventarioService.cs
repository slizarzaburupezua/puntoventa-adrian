using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Medida.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Response;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Interface
{
    public interface IInventarioService
    {
        #region MEDIDA

        Task<IList<MedidaDTO>> GetAllMedidaAsync(Guid idUsuarioGuid);

        #endregion

        #region CATEGORIA

        Task<IList<CategoriaDTO>> GetAllCategoriaByFilterAsync(ObtenerCategoriaRequest request);

        Task<IList<CategoriaDTO>> GetAllCategoriasForComboBoxAsync();

        Task<ResponseDTO> InsertCategoriaAsync(RegistrarCategoriaRequest request);

        Task<ResponseDTO> UpdateCategoriaAsync(ActualizarCategoriaRequest request);

        Task<ResponseDTO> UpdateActivoCategoriaAsync(ActualizarActivoCategoriaRequest request);

        Task<ResponseDTO> DeleteCategoriaAsync(EliminarCategoriaRequest request);

        #endregion

        #region PRODUCTO

        Task<List<ProductoDTO>> GetProductoByCodeAsync(ObtenerProductoPorCodigoRequest request);

        Task<IList<ProductoDTO>> GetAllProductoByFilterAsync(ObtenerProductoRequest request);

        Task<IList<ProductoDTO>> GetAllProductoForComboBoxAsync();

        Task<ResponseDTO> InsertProductoAsync(RegistrarProductoRequest request);

        Task<ResponseDTO> UpdateProductoAsync(ActualizarProductoRequest request);

        Task<ResponseDTO> UpdateActivoProductoAsync(ActualizarActivoProductoRequest request);

        Task<ResponseDTO> DeleteProductoAsync(EliminarProductoRequest request);

        Task<IList<ProductoDTO>> GetAllProductsByCategoryAsync(int idCategory);

        Task<IList<CategoriaConConteoDTO>> GetCategoriesWithProductsCountAsync();

        #endregion

        #region MARCA

        Task<IList<MarcaDTO>> GetAllMarcaByFilterAsync(ObtenerMarcaRequest request);

        Task<IList<MarcaDTO>> GetAllMarcasForComboBoxAsync();

        Task<ResponseDTO> InsertMarcaAsync(RegistrarMarcaRequest request);

        Task<ResponseDTO> UpdateMarcaAsync(ActualizarMarcaRequest request);

        Task<ResponseDTO> UpdateActivoMarcaAsync(ActualizarActivoMarcaRequest request);

        Task<ResponseDTO> DeleteMarcaAsync(EliminarMarcaRequest request);

        #endregion

    }
}
