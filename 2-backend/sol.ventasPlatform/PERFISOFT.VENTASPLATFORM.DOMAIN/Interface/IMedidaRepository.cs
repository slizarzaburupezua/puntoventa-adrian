using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IMedidaRepository
    {
        Task<IList<Medida>?> SelectAllAsync();
    }
}
