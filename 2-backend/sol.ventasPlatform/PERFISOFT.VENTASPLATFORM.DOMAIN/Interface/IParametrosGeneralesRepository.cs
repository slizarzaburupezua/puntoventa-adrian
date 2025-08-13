using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Menu.Consulta;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IParametrosGeneralesRepository
    {
        Task<List<Rol>> GetAllRolAsync();

        Task<List<Menu>> GetAllMenuAsync();

        Task<List<Moneda>> GetAllMonedaAsync();

        Task<List<TipoDocumento>> GetAllTipoDocumentoAsync();

        Task<List<Genero>> GetAllGeneroAsync();

        Task<Negocio> GetNegocioAsync();

        Task<List<ObtenerMenuRol>> GetAllMenuRolAsync(int idRol);

        Task<int> GetIdMonedaNegocioAsync();

        Task<Moneda> GetMonedaByIdAsync(int idMoneda);

        Task UpdateNegocioAsync(Negocio negocio);

        Task<string> GetCultureInfoAsync();

        Task<Moneda> GetMonedaByCodigoAsync(string codMoneda);

    }
}
