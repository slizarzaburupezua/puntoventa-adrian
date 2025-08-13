using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Filtro;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IProductoRepository
    {
        Task<int> CountByFechaRegistroAsync(DateTime? fechaInicio, DateTime? fechaFin);

        Task<List<Producto>> SelectAllByFilterAsync(FiltroConsultaProducto filtro);

        Task<List<Producto>> SelectAllForComboBoxAsync();

        Task<Producto?> SelectByIdAsync(int idProducto);

        Task<List<Producto>> SelectByIdsAsync(List<int> idsProducto);

        Task<List<Producto>> SelectAllByCodeAsync(string parametro);

        Task<Producto> InsertAsync(Producto producto);

        Task<Producto> UpdateAsync(Producto producto);

        Task UpdateActivoAsync(int idProducto, bool flgActivo);

        Task UpdateAsync(List<Producto> producto);

        Task<bool> ExistCodigoProductoAsync(string codigoProducto);

        Task<bool> ExistNombreProductoAsync(string nombreProducto);

        Task<List<Producto>> SelectByCategoryAsync(int idCategory);

        Task<List<CategoriaConConteoVO>> SelectCategoriesWithProductsCountAsync();
    }
}
