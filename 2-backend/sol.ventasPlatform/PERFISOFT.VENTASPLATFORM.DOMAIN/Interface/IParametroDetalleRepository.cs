using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IParametroDetalleRepository
    {
        Task<string> SelectValueBySubKeyAsync(string key, string subKey);

        Task<List<ParametroDetalle>> SelectAllByIdAsync(int idParametro);

        Task<List<ParametroDetalle>> SelectAllByKeyAsync(string key);

        Task<ParametroDetalle?> SelecByIdAsync(int id);

        Task<ParametroDetalle> UpdateAsync(ParametroDetalle parametroDetalle);
    }
}
