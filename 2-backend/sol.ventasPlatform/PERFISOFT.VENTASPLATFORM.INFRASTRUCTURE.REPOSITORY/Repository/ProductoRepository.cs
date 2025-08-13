using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class ProductoRepository : BaseRepository, IProductoRepository
    {
        private VentasPlatformContext _context;
        public ProductoRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountByFechaRegistroAsync(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await All<Producto>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado)
                .Where(p => !fechaInicio.HasValue || p.FECHA_REGISTRO.Date >= fechaInicio)
                .Where(p => !fechaFin.HasValue || p.FECHA_REGISTRO.Date <= fechaFin)
                .CountAsync();
        }

        public async Task<List<Producto>> SelectAllByFilterAsync(FiltroConsultaProducto filtro)
        {
            return await All<Producto>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado
                )
                .Where(p => string.IsNullOrEmpty(filtro.CODIGO) || p.NOMBRE.Contains(filtro.CODIGO))
                .Where(p => string.IsNullOrEmpty(filtro.NOMBRE) || p.NOMBRE.Contains(filtro.NOMBRE))
                .Where(p => !filtro.FECHA_REGISTRO_INICIO.HasValue || p.FECHA_REGISTRO.Date >= filtro.FECHA_REGISTRO_INICIO)
                .Where(p => !filtro.FECHA_REGISTRO_FIN.HasValue || p.FECHA_REGISTRO.Date <= filtro.FECHA_REGISTRO_FIN)
                .Where(p => !filtro.PRECIO_COMPRA_INICIO.HasValue || p.PRECIO_COMPRA >= filtro.PRECIO_COMPRA_INICIO)
                .Where(p => !filtro.PRECIO_COMPRA_FIN.HasValue || p.PRECIO_COMPRA <= filtro.PRECIO_COMPRA_FIN)
                .Where(p => !filtro.PRECIO_VENTA_INICIO.HasValue || p.PRECIO_VENTA >= filtro.PRECIO_VENTA_INICIO)
                .Where(p => !filtro.PRECIO_VENTA_FIN.HasValue || p.PRECIO_VENTA <= filtro.PRECIO_VENTA_FIN)
                .Where(p => filtro.LST_CATEGORIAS == null || filtro.LST_CATEGORIAS.Length == Numeracion.Cero || filtro.LST_CATEGORIAS.Contains(p.CATEGORIA.ID))
                .Where(p => filtro.LST_MARCAS == null || filtro.LST_MARCAS.Length == Numeracion.Cero || filtro.LST_MARCAS.Contains(p.MARCA.ID))
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
                .Include(p => p.MARCA)
                .Include(p => p.CATEGORIA)
                .ToListAsync();
        }

        public async Task<List<Producto>> SelectAllForComboBoxAsync()
        {
            return await All<Producto>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado
                )
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
                .Select(x => new Producto()
                {
                    ID = x.ID,
                    COLOR = x.COLOR,
                    NOMBRE = x.NOMBRE,
                })
                .ToListAsync();
        }

        public async Task<List<CategoriaConConteoVO>> SelectCategoriesWithProductsCountAsync()
        {
            var categorias = await All<Producto>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado &&
                            p.ACTIVO == Flags.Activado &&
                            p.CATEGORIA.ESTADO == Flags.Habilitado &&
                            p.CATEGORIA.ACTIVO == Flags.Activado)
                .GroupBy(p => new { p.CATEGORIA.ID, p.CATEGORIA.NOMBRE })
                .Select(g => new CategoriaConConteoVO
                {
                    IdCategoria = g.Key.ID,
                    Nombre = g.Key.NOMBRE,
                    CantidadProductos = g.Count()
                })
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            var totalProductos = categorias.Sum(c => c.CantidadProductos);
            categorias.Insert(Numeracion.Cero, new CategoriaConConteoVO
            {
                IdCategoria = Numeracion.Cero,
                Nombre = Strings.Todos,
                CantidadProductos = totalProductos
            });

            return categorias;
        }

        public async Task<List<Producto>> SelectByCategoryAsync(int idCategoria)
        {
            var query = All<Producto>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado &&
                            p.CATEGORIA.ESTADO == Flags.Habilitado &&
                            p.CATEGORIA.ACTIVO == Flags.Activado);

            if (idCategoria > Numeracion.Cero)
                query = query.Where(p => p.CATEGORIA.ID == idCategoria);

            return await query
                .OrderByDescending(p => p.FECHA_REGISTRO)
                .Include(p => p.MARCA)
                .Include(p => p.CATEGORIA)
                .ToListAsync();
        }

        public async Task<Producto?> SelectByIdAsync(int idProducto)
        {
            return await All<Producto>()
                .AsNoTracking()
                .Where(u => u.ID == idProducto
                     && u.ESTADO == Flags.Habilitado
                )
                .FirstOrDefaultAsync();
        }

        public async Task<List<Producto>> SelectByIdsAsync(List<int> idsProducto)
        {
            return await All<Producto>()
                .AsNoTracking()
                .Where(u => idsProducto.Contains(u.ID)
                        && u.ESTADO == Flags.Habilitado
                        && u.CATEGORIA.ESTADO == Flags.Habilitado
                        && u.CATEGORIA.ACTIVO == Flags.Activado
                )
                .ToListAsync();
        }

        public async Task<List<Producto>> SelectAllByCodeAsync(string parametro)
        {
            return await All<Producto>()
                .AsNoTracking()
                .Where(p =>
                            p.ACTIVO == Flags.Activado &&
                            p.ESTADO == Flags.Habilitado &&
                            p.CATEGORIA.ESTADO == Flags.Habilitado &&
                            p.CATEGORIA.ACTIVO == Flags.Activado)
                .Where(p => string.IsNullOrEmpty(parametro.Trim()) ||
                            p.NOMBRE.Contains(parametro.Trim()) ||
                            p.CODIGO.Contains(parametro.Trim()) ||
                            p.CATEGORIA.NOMBRE.Contains(parametro.Trim()) ||
                            p.MARCA.NOMBRE.Contains(parametro.Trim()))
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
                .Include(p => p.MARCA)
                .Include(p => p.CATEGORIA)
                .ToListAsync();
        }


        public async Task<Producto> InsertAsync(Producto producto)
        {
            await base.InsertAsync(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> ExistCodigoProductoAsync(string codigoProducto)
        {
            return await All<Producto>()
                         .AsNoTracking()
                         .AnyAsync(c => c.CODIGO == codigoProducto);
        }

        public async Task<bool> ExistNombreProductoAsync(string nombreProducto)
        {
            return await All<Producto>()
                         .AsNoTracking()
                         .AnyAsync(c => c.NOMBRE == nombreProducto);
        }

        public async Task<Producto> UpdateAsync(Producto producto)
        {
            await base.UpdateAsync(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task UpdateAsync(List<Producto> producto)
        {
            await base.UpdateAsync(producto);
        }

        public async Task UpdateActivoAsync(int idProducto, bool flgActivo)
        {
            await _context.Producto.Where(x => x.ID == idProducto).ExecuteUpdateAsync(u => u.SetProperty(e => e.ACTIVO, flgActivo));
        }
    }
}
