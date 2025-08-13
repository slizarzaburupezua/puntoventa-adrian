using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Menu.Consulta;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class ParametrosGeneralesRepository : BaseRepository, IParametrosGeneralesRepository
    {
        private VentasPlatformContext _context;
        public ParametrosGeneralesRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Rol>> GetAllRolAsync()
        {
            return await All<Rol>()
                .AsNoTracking()
                .OrderBy(x => x.NOMBRE)
                .ToListAsync();
        }

        public async Task<List<Menu>> GetAllMenuAsync()
        {
            return await All<Menu>()
                .AsNoTracking()
                .OrderBy(x => x.ORDEN)
                .ToListAsync();
        }

        public async Task<List<Moneda>> GetAllMonedaAsync()
        {
            return await All<Moneda>()
                .AsNoTracking()
                .Where(m =>
                    All<Moneda>()
                    .Where(x => x.CODIGO_MONEDA == m.CODIGO_MONEDA)
                    .OrderBy(x => x.ORDEN)
                    .Select(x => x.ID)
                    .First() == m.ID)
                .OrderBy(m => m.DESCRIPCION)
                .ToListAsync();
        }

        public async Task<Moneda> GetMonedaByCodigoAsync(string codMoneda)
        {
            return await All<Moneda>()
                .AsNoTracking()
                .Where(m => m.CODIGO_MONEDA == codMoneda)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TipoDocumento>> GetAllTipoDocumentoAsync()
        {
            return await All<TipoDocumento>()
                .AsNoTracking()
                .OrderBy(x => x.ORDEN)
                .ToListAsync();
        }

        public async Task<List<Genero>> GetAllGeneroAsync()
        {
            return await All<Genero>()
                .AsNoTracking()
                .OrderBy(x => x.ORDEN)
                .ToListAsync();
        }

        public async Task<Negocio> GetNegocioAsync()
        {
            return await All<Negocio>()
                .AsNoTracking()
                .Include(p => p.MONEDA)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetIdMonedaNegocioAsync()
        {
            return await All<Negocio>().Select(x => x.ID_MONEDA).FirstOrDefaultAsync();
        }

        public async Task<Moneda> GetMonedaByIdAsync(int idMoneda)
        {
            return await All<Moneda>().Where(x => x.ID == idMoneda).FirstOrDefaultAsync();
        }

        public async Task<string> GetCultureInfoAsync()
        {
            return await All<Negocio>().Include(x => x.MONEDA).Select(x => x.MONEDA.CULTUREINFO).FirstOrDefaultAsync();
        }

        public async Task<List<ObtenerMenuRol>> GetAllMenuRolAsync(int idRol)
        {
            return await (from mr in _context.MenuRol
                          join m in _context.Menu on mr.ID_MENU equals m.ID
                          where mr.ID_ROL == idRol
                          select new ObtenerMenuRol
                          {
                              PADRE_TEXTO = m.PADRE_TEXTO,
                              HIJO_TEXTO = m.HIJO_TEXTO,
                              TITULO = m.TITULO,
                              TIPO = m.TIPO,
                              ICONO = m.ICONO,
                              FLG_ENLACE_EXTERNO = m.FLG_ENLACE_EXTERNO,
                              FLG_MENU_HIJO = m.FLG_MENU_HIJO,
                              RUTA = m.RUTA,
                              ORDEN = m.ORDEN,

                          }).ToListAsync();
        }

        public async Task UpdateNegocioAsync(Negocio negocio)
        {
            await base.UpdateAsync(negocio);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Parametro>> GetAllParametroAsync()
        {
            return await All<Parametro>().AsNoTracking().ToListAsync();
        }

    }
}
