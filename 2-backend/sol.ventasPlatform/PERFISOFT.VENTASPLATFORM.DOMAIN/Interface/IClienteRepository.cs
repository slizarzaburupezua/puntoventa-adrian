using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Cliente.Filtro;

namespace PERFISOFT.VENTASPLATFORM.DOMAIN.Interface
{
    public interface IClienteRepository
    {
        Task<int> CountByFechaRegistroAsync(DateTime? fechaInicio, DateTime? fechaFin);

        Task<List<Cliente>> SelectAllByFilterAsync(FiltroConsultaCliente filtro);

        Task<Cliente?> SelectByIdAsync(int idCliente);

        Task<Cliente> InsertAsync(Cliente cliente);

        Task<Cliente> UpdateAsync(Cliente cliente);

        Task<bool> ExistCorreoAsync(string correo);

        Task<bool> ExistNumeroDocumentoAsync(string numeroDocumento);

        Task UpdateActivoAsync(int idCliente, bool flgActivo);

        Task<Cliente> SelectByNumDocumentoCorreoAsync(string parametro);
    }
}
