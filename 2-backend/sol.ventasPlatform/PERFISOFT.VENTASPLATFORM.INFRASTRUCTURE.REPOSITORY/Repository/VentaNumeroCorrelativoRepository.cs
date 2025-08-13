using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class VentaNumeroCorrelativoRepository : BaseRepository, IVentaNumeroCorrelativoRepository
    {
        private VentasPlatformContext _context;
        public VentaNumeroCorrelativoRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<VentaNumeroCorrelativo> SelectNextCorrelativoAsync(string serie)
        {
            return await All<VentaNumeroCorrelativo>()
                .AsNoTracking()
                .Where(c => c.SERIE == serie)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateCorrelativoAsync(string serie)
        {
            var correlativo = await All<VentaNumeroCorrelativo>()
                .AsNoTracking()
                .Where(c => c.SERIE == serie)
                .FirstOrDefaultAsync();

            correlativo.NUMERO += Numeracion.Uno;

            await base.UpdateAsync(correlativo);
        }

    }

}
