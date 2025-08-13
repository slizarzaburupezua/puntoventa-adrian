using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class ParametroDetalleRepository : BaseRepository, IParametroDetalleRepository
    {
        private VentasPlatformContext _context;
        public ParametroDetalleRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> SelectValueBySubKeyAsync(string key, string subKey)
        {
            return await All<ParametroDetalle>().AsNoTracking().Where(x => x.PARA_KEY == key && x.SUB_PARA_KEY == subKey).Select(x => x.SVALOR2).FirstOrDefaultAsync();
        }

        public async Task<List<ParametroDetalle>> SelectAllByIdAsync(int idParametro)
        {
            return await All<ParametroDetalle>().AsNoTracking().Where(x => x.ID_PARAMETRO == idParametro).ToListAsync();
        }

        public async Task<List<ParametroDetalle>> SelectAllByKeyAsync(string key)
        {
            return await All<ParametroDetalle>().AsNoTracking().Where(x => x.PARA_KEY == key).ToListAsync();
        }

        public async Task<ParametroDetalle?> SelecByIdAsync(int id)
        {
            return await All<ParametroDetalle>()
                .AsNoTracking()
                .Where(u => u.ID == id
                     && u.ESTADO == Flags.Habilitado
                )
                .FirstOrDefaultAsync();
        }

        public async Task<ParametroDetalle> UpdateAsync(ParametroDetalle parametroDetalle)
        {
            await base.UpdateAsync(parametroDetalle);
            await _context.SaveChangesAsync();
            return parametroDetalle;
        }

    }
}
