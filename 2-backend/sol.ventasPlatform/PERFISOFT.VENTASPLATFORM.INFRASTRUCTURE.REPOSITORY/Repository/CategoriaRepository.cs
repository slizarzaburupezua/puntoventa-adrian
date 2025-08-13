using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class CategoriaRepository : BaseRepository, ICategoriaRepository
    {
        private VentasPlatformContext _context;
        public CategoriaRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Categoria?> SelectByIdAsync(int idCategoria)
        {
            return await All<Categoria>()
                .AsNoTracking()
                .Where(c => c.ID == idCategoria && c.ESTADO == Flags.Habilitado)
                .OrderByDescending(x => x.FECHA_REGISTRO)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Categoria>> SelectAllByNameAsync(string nombre)
        {
            return await All<Categoria>()
                .AsNoTracking()
                .Where(i => i.ESTADO == Flags.Habilitado)
                .Where(i => string.IsNullOrEmpty(nombre) || i.NOMBRE.Contains(nombre))
                .OrderByDescending(i => i.ACTIVO)
                .ThenByDescending(i => i.FECHA_REGISTRO)
                .Include(i => i.MEDIDA)
                .ToListAsync();
        }

        public async Task<List<Categoria>> SelectAllForComboBoxAsync()
        {
            return await All<Categoria>()
                .AsNoTracking()
                .Where(i => i.ESTADO == Flags.Habilitado)
                .OrderByDescending(i => i.ACTIVO)
                .ThenByDescending(i => i.FECHA_REGISTRO)
                .Include(i => i.MEDIDA)
                .Select(x => new Categoria()
                {
                    ID = x.ID,
                    COLOR = x.COLOR,
                    NOMBRE = x.NOMBRE,
                })
                .ToListAsync();
        }

        public async Task<Categoria> InsertAsync(Categoria categoria)
        {
            await base.InsertAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> UpdateAsync(Categoria categoria)
        {
            await base.UpdateAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task UpdateActivoAsync(int idCategoria, bool flgActivo)
        {
            await _context.Categoria.Where(x => x.ID == idCategoria).ExecuteUpdateAsync(u => u.SetProperty(e => e.ACTIVO, flgActivo));
        }

        public async Task<bool> ExistProductoCategoriaAsync(int idCategoria, int idUsuario)
        {
            return await All<Producto>()
            .AsNoTracking()
               .Where(p => p.ESTADO == Flags.Habilitado && p.CATEGORIA.ID == idCategoria)
                .AnyAsync();
        }
    }
}
