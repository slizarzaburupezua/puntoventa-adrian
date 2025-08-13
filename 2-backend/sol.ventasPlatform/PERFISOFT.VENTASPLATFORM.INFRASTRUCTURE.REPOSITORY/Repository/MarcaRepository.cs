using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class MarcaRepository : BaseRepository, IMarcaRepository
    {
        private VentasPlatformContext _context;
        public MarcaRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Marca>> SelectAllByNameAsync(string nombre)
        {
            return await All<Marca>()
                .AsNoTracking()
                .Where(i => i.ESTADO == Flags.Habilitado)
                .Where(i => string.IsNullOrEmpty(nombre) || i.NOMBRE.Contains(nombre))
                .OrderByDescending(i => i.ACTIVO)
                .ThenByDescending(i => i.FECHA_REGISTRO)
                .ToListAsync();
        }

        public async Task<List<Marca>> SelectAllForComboBoxAsync()
        {
            return await All<Marca>()
                .AsNoTracking()
                .Where(i => i.ESTADO == Flags.Habilitado)
                .OrderByDescending(i => i.ACTIVO)
                .ThenByDescending(i => i.FECHA_REGISTRO)
                .Select(x => new Marca()
                {
                    ID = x.ID,
                    COLOR = x.COLOR,
                    NOMBRE = x.NOMBRE,
                })
                .ToListAsync();
        }

        public async Task<Marca?> SelectByIdAsync(int idMarca)
        {
            return await All<Marca>()
                .AsNoTracking()
                .Where(u => u.ID == idMarca
                     && u.ESTADO == Flags.Habilitado
                )
                .FirstOrDefaultAsync();
        }

        public async Task<Marca> InsertAsync(Marca Marca)
        {
            await base.InsertAsync(Marca);
            await _context.SaveChangesAsync();
            return Marca;
        }

        public async Task<Marca> UpdateAsync(Marca Marca)
        {
            await base.UpdateAsync(Marca);
            await _context.SaveChangesAsync();
            return Marca;
        }

        public async Task UpdateActivoAsync(int idMarca, bool flgActivo)
        {
            await _context.Marca.Where(x => x.ID == idMarca).ExecuteUpdateAsync(u => u.SetProperty(e => e.ACTIVO, flgActivo));
        }

        public async Task<bool> ExistProductoMarcaAsync(int idMarca, int idUsuario)
        {
            return await All<Producto>()
            .AsNoTracking()
               .Where(p => p.ESTADO == Flags.Habilitado && p.MARCA.ID == idMarca)
                .AnyAsync();
        }
    }
}
