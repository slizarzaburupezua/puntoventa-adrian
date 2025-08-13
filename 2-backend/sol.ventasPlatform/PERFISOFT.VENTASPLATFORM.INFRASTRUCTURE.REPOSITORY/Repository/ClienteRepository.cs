using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Cliente.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class ClienteRepository : BaseRepository, IClienteRepository
    {
        private VentasPlatformContext _context;
        public ClienteRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountByFechaRegistroAsync(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await All<Cliente>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado)
                .Where(p => !fechaInicio.HasValue || p.FECHA_REGISTRO.Date >= fechaInicio)
                .Where(p => !fechaFin.HasValue || p.FECHA_REGISTRO.Date <= fechaFin)
                .CountAsync();
        }

        public async Task<List<Cliente>> SelectAllByFilterAsync(FiltroConsultaCliente filtro)
        {
            return await All<Cliente>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado &&
                        p.TIPODOCUMENTO.ESTADO == Flags.Habilitado &&
                        p.TIPODOCUMENTO.ACTIVO == Flags.Activado
                )
                .Where(p => string.IsNullOrEmpty(filtro.NUMERO_DOCUMENTO) || p.NUMERO_DOCUMENTO.Contains(filtro.NUMERO_DOCUMENTO))
                .Where(p => string.IsNullOrEmpty(filtro.NOMBRES) || p.NOMBRES.Contains(filtro.NOMBRES))
                .Where(p => string.IsNullOrEmpty(filtro.APELLIDOS) || p.APELLIDOS.Contains(filtro.APELLIDOS))
                .Where(p => string.IsNullOrEmpty(filtro.CELULAR) || p.CELULAR.Contains(filtro.CELULAR))
                .Where(p => string.IsNullOrEmpty(filtro.DIRECCION) || p.DIRECCION.Contains(filtro.DIRECCION))
                .Where(p => string.IsNullOrEmpty(filtro.CORREO_ELECTRONICO) || p.CORREO_ELECTRONICO.Contains(filtro.CORREO_ELECTRONICO))
                .Where(p => !filtro.FECHA_REGISTRO_INICIO.HasValue || p.FECHA_REGISTRO.Date >= filtro.FECHA_REGISTRO_INICIO)
                .Where(p => !filtro.FECHA_REGISTRO_FIN.HasValue || p.FECHA_REGISTRO.Date <= filtro.FECHA_REGISTRO_FIN)
                .Where(p => filtro.LST_TIPODOCUMENTO == null || filtro.LST_TIPODOCUMENTO.Length == Numeracion.Cero || filtro.LST_TIPODOCUMENTO.Contains(p.TIPODOCUMENTO.ID))
                .Where(p => filtro.LST_GENERO == null || filtro.LST_GENERO.Length == Numeracion.Cero || filtro.LST_GENERO.Contains(p.GENERO.ID))
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
                .Include(p => p.TIPODOCUMENTO)
                .Include(p => p.GENERO)
                .ToListAsync();
        }

        public async Task<Cliente?> SelectByIdAsync(int idCliente)
        {
            return await All<Cliente>()
                .AsNoTracking()
                .Where(u => u.ID == idCliente
                     && u.ESTADO == Flags.Habilitado
                     && u.TIPODOCUMENTO.ESTADO == Flags.Habilitado
                     && u.TIPODOCUMENTO.ACTIVO == Flags.Activado
                )
                .FirstOrDefaultAsync();
        }

        public async Task<Cliente> SelectByNumDocumentoCorreoAsync(string parametro)
        {
            return await All<Cliente>()
                .AsNoTracking()
                .Where(c => c.ESTADO == Flags.Habilitado)
                .Where(c => string.IsNullOrEmpty(parametro.Trim()) ||
                            c.NUMERO_DOCUMENTO.Contains(parametro.Trim()) ||
                            c.CORREO_ELECTRONICO.Contains(parametro.Trim()))
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
                .FirstOrDefaultAsync();
        }

        public async Task<Cliente> InsertAsync(Cliente cliente)
        {
            await base.InsertAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            await base.UpdateAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task UpdateActivoAsync(int idCliente, bool flgActivo)
        {
            await _context.Cliente.Where(x => x.ID == idCliente).ExecuteUpdateAsync(u => u.SetProperty(e => e.ACTIVO, flgActivo));
        }

        public async Task<bool> ExistCorreoAsync(string correo)
        {
            return await All<Cliente>()
                         .AsNoTracking()
                         .AnyAsync(c => c.CORREO_ELECTRONICO == correo && c.ESTADO);
        }

        public async Task<bool> ExistNumeroDocumentoAsync(string numeroDocumento)
        {
            return await All<Cliente>()
                         .AsNoTracking()
                         .AnyAsync(c => c.NUMERO_DOCUMENTO == numeroDocumento && c.ESTADO);
        }
    }
}
