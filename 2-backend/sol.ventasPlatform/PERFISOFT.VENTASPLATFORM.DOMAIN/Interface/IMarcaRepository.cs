using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> SelectAllByNameAsync(string nombre);

        Task<List<Marca>> SelectAllForComboBoxAsync();

        Task<Marca?> SelectByIdAsync(int idMarca);

        Task<Marca> InsertAsync(Marca Marca);

        Task<Marca> UpdateAsync(Marca Marca);

        Task UpdateActivoAsync(int idMarca, bool flgActivo);

        Task<bool> ExistProductoMarcaAsync(int idMarca, int idUsuario);


    }
}
