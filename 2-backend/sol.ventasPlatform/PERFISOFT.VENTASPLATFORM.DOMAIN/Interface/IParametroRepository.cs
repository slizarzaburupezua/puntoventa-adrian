using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IParametroRepository
    {
        Task<List<Parametro>> SelectAllAsync();


    }
}
