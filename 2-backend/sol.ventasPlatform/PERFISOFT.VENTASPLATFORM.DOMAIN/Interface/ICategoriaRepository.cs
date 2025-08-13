using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface ICategoriaRepository
    {
        Task<Categoria?> SelectByIdAsync(int idCategoria);

        Task<List<Categoria>> SelectAllByNameAsync(string nombre);

        Task<List<Categoria>> SelectAllForComboBoxAsync();

        Task<Categoria> InsertAsync(Categoria categoria);

        Task<Categoria> UpdateAsync(Categoria categoria);

        Task UpdateActivoAsync(int idCategoria, bool flgActivo);

        Task<bool> ExistProductoCategoriaAsync(int idCategoria, int idUsuario);
    }
}
