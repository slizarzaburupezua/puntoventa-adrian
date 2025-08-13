using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository
{
    public class VentaRepository : BaseRepository, IVentaRepository
    {
        private VentasPlatformContext _context;

        public VentaRepository(VentasPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> SelectTotalVentasAsync(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await All<Venta>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Habilitado)
                .Where(p => p.ESTADO == Flags.Activado)
                .Where(p => !fechaInicio.HasValue || p.FECHA_VENTA.Date >= fechaInicio.Value.Date)
                .Where(p => !fechaFin.HasValue || p.FECHA_VENTA.Date <= fechaFin.Value.Date)
                .SumAsync(p => (decimal?)p.PRECIO_TOTAL) ?? 0m;
        }

        public async Task<decimal> SelectTotalVentasAnuladasAsync(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return await All<Venta>()
                .AsNoTracking()
                .Where(p => p.ESTADO == Flags.Desactivado)
                .Where(p => !fechaInicio.HasValue || p.FECHA_VENTA.Date >= fechaInicio.Value.Date)
                .Where(p => !fechaFin.HasValue || p.FECHA_VENTA.Date <= fechaFin.Value.Date)
                .SumAsync(p => (decimal?)p.PRECIO_TOTAL) ?? 0m;
        }

        public async Task<Venta?> SelectByIdAsync(int idVenta)
        {
            return await All<Venta>()
                .AsNoTracking()
                .Where(u => u.ID == idVenta
                     && u.ESTADO == Flags.Habilitado
                )
                .FirstOrDefaultAsync();
        }

        public async Task<IList<ConsultaVentaByFilter>> SelectAllByFilterAsync(FiltroObtenerVenta filtro)
        {
            return await All<Venta>()
                .AsNoTracking()
                .Where(p => string.IsNullOrEmpty(filtro.NumeroVenta) || p.NUMERO_VENTA.Contains(filtro.NumeroVenta))
                .Where(p => !filtro.FechaVentaInicio.HasValue || p.FECHA_VENTA.Date >= filtro.FechaVentaInicio)
                .Where(p => !filtro.FechaVentaFin.HasValue || p.FECHA_REGISTRO.Date <= filtro.FechaVentaFin)
                .Where(p => !filtro.MontoVentaInicio.HasValue || p.PRECIO_TOTAL >= filtro.MontoVentaFin)
                .Where(p => !filtro.MontoVentaFin.HasValue || p.PRECIO_TOTAL <= filtro.MontoVentaFin)
                .Where(p => filtro.LstUsuario == null || filtro.LstUsuario.Length == Numeracion.Cero || filtro.LstUsuario.Contains(p.USUARIO.CORREO_ELECTRONICO))
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
            .Select(p => new ConsultaVentaByFilter
            {
                IdVenta = p.ID,
                NumeroVenta = p.NUMERO_VENTA,
                NombreCliente = $"{p.CLIENTE.NOMBRES} {p.CLIENTE.APELLIDOS}",
                CorreoUsuario = p.USUARIO.CORREO_ELECTRONICO,
                FechaVenta = p.FECHA_VENTA,
                TotalVenta = p.PRECIO_TOTAL,
                UrlBoletaFactura = p.URLBOLETAFACTURA,
                Estado = p.ESTADO
            }).ToListAsync();
        }

        public async Task<Venta> InsertAsync(Venta venta)
        {
            await base.InsertAsync(venta);
            await _context.SaveChangesAsync();

            return await _context.Venta
                .Include(v => v.CLIENTE)
                .FirstOrDefaultAsync(v => v.ID == venta.ID);
        }

        public async Task UpdateAsync(Venta venta)
        {
            await base.UpdateAsync(venta);
            await _context.SaveChangesAsync();
        }
    }
}
