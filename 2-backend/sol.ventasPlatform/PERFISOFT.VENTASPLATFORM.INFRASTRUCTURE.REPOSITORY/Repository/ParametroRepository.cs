using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class ParametroRepository : BaseRepository, IParametroRepository
    {
        private VentasPlatformContext _context;
        public ParametroRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Parametro>> SelectAllAsync()
        {
            return await All<Parametro>().AsNoTracking().ToListAsync();
        }

    }
}
