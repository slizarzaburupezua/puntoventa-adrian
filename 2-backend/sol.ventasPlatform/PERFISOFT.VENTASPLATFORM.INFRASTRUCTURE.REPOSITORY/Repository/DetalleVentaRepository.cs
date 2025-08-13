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
    public class DetalleVentaRepository : BaseRepository, IDetalleVentaRepository
    {
        public DetalleVentaRepository(VentasPlatformContext context) : base(context)
        {
        }

        public async Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroDetalleVentaProductoReporte filtro)
        {
            return await All<DetalleVenta>()
                .AsNoTracking()
                .Where(v => v.ESTADO == Flags.Habilitado
                    && v.ACTIVO == Flags.Activado
                    && v.VENTA.ESTADO == Flags.Habilitado
                    && v.VENTA.ACTIVO == Flags.Activado)
                .Where(v => !filtro.FechaVentaInicio.HasValue || v.FECHA_REGISTRO.Date >= filtro.FechaVentaInicio)
                .Where(v => !filtro.FechaVentaFin.HasValue || v.FECHA_REGISTRO.Date <= filtro.FechaVentaFin)
                .Where(v => filtro.LstProductos == null || filtro.LstProductos.Length == Numeracion.Cero || filtro.LstProductos.Contains(v.PRODUCTO.ID))
                .OrderBy(v => v.FECHA_REGISTRO)
                .Include(v => v.PRODUCTO.CATEGORIA)
                .Include(v => v.PRODUCTO.MARCA)
                .ToListAsync();
        }

        public async Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroDetalleVentaCategoriaReporte filtro)
        {
            return await All<DetalleVenta>()
                .AsNoTracking()
                            .Where(v => v.ESTADO == Flags.Habilitado
                    && v.ACTIVO == Flags.Activado
                    && v.VENTA.ESTADO == Flags.Habilitado
                    && v.VENTA.ACTIVO == Flags.Activado)
                .Where(v => !filtro.FechaVentaInicio.HasValue || v.FECHA_REGISTRO.Date >= filtro.FechaVentaInicio)
                .Where(v => !filtro.FechaVentaFin.HasValue || v.FECHA_REGISTRO.Date <= filtro.FechaVentaFin)
                .Where(v => filtro.LstCategorias == null || filtro.LstCategorias.Length == Numeracion.Cero || filtro.LstCategorias.Contains(v.PRODUCTO.CATEGORIA.ID))
                .OrderBy(v => v.FECHA_REGISTRO)
                .Include(v => v.PRODUCTO.CATEGORIA)
                .Include(v => v.PRODUCTO.MARCA)
                .ToListAsync();
        }

        public async Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroDetalleVentaMarcaReporte filtro)
        {
            return await All<DetalleVenta>()
                .AsNoTracking()
                .Where(v => v.ESTADO == Flags.Habilitado
                    && v.ACTIVO == Flags.Activado
                    && v.VENTA.ESTADO == Flags.Habilitado
                    && v.VENTA.ACTIVO == Flags.Activado)
                .Where(v => !filtro.FechaVentaInicio.HasValue || v.FECHA_REGISTRO.Date >= filtro.FechaVentaInicio)
                .Where(v => !filtro.FechaVentaFin.HasValue || v.FECHA_REGISTRO.Date <= filtro.FechaVentaFin)
                .Where(v => filtro.LstMarcas == null || filtro.LstMarcas.Length == Numeracion.Cero || filtro.LstMarcas.Contains(v.PRODUCTO.MARCA.ID))
                .OrderBy(v => v.FECHA_REGISTRO)
                .Include(v => v.PRODUCTO.CATEGORIA)
                .Include(v => v.PRODUCTO.MARCA)
                .ToListAsync();
        }

        public async Task<List<DetalleVenta>> SelectAllForReportsByFilterAsync(FiltroResumenReporte filtro)
        {
            return await All<DetalleVenta>()
                .AsNoTracking()
                .Where(v => v.ESTADO == Flags.Habilitado
                    && v.ACTIVO == Flags.Activado
                    && v.VENTA.ESTADO == Flags.Habilitado
                    && v.VENTA.ACTIVO == Flags.Activado)
                .Where(v => !filtro.FechaVentaInicio.HasValue || v.FECHA_REGISTRO.Date >= filtro.FechaVentaInicio)
                .Where(v => !filtro.FechaVentaFin.HasValue || v.FECHA_REGISTRO.Date <= filtro.FechaVentaFin)
                .OrderBy(v => v.FECHA_REGISTRO)
                .Include(v => v.PRODUCTO.CATEGORIA)
                .Include(v => v.PRODUCTO.MARCA)
                .ToListAsync();
        }

        public async Task<IList<ConsultaDetalleVentaByIdVenta>> SelectDetalleAsync(int idVenta)
        {
            var query = await All<DetalleVenta>()
                .AsNoTracking()
                .Where(p => p.ID_VENTA == idVenta)
                .OrderByDescending(p => p.ACTIVO)
                .ThenByDescending(p => p.FECHA_REGISTRO)
            .Select(p => new ConsultaDetalleVentaByIdVenta
            {
                IdProducto = p.ID_PRODUCTO,
                NombreProducto = p.NOMBRE_PRODUCTO,
                ColorProducto = p.COLOR_PRODUCTO,
                NombreCategoria = p.NOMBRE_CATEGORIA,
                ColorCategoria = p.COLOR_CATEGORIA,
                NombreMarca = p.NOMBRE_MARCA,
                ColorMarca = p.COLOR_MARCA,
                Cantidad = p.CANTIDAD,
                PrecioProducto = p.PRECIO_VENTA,
                PrecioTotal = p.PRECIO_TOTAL,
                UrlFotoProducto = p.URLFOTO_PRODUCTO,
            }).ToListAsync();
            return query;
        }

        public async Task<IList<ConsultaDetalleVentaByIdVenta>> SelectDetalleAsync(int? idVenta,
                                                                            string[] LstUsuario,
                                                                            string? numeroVenta,
                                                                            DateTime? fechaVentaInicio,
                                                                            DateTime? fechaVentaFin)
        {
            var usuarios = LstUsuario?.ToList() ?? new List<string>();

            return await All<Venta>()
            .AsNoTracking()
            .Where(v => !idVenta.HasValue || idVenta.Value <= 0 || v.ID == idVenta.Value)
            .Where(v => !usuarios.Any() || usuarios.Contains(v.USUARIO.CORREO_ELECTRONICO))
            .Where(v => string.IsNullOrEmpty(numeroVenta) || v.NUMERO_VENTA == numeroVenta)
            .Where(v => !fechaVentaInicio.HasValue || v.FECHA_VENTA >= fechaVentaInicio.Value)
            .Where(v => !fechaVentaFin.HasValue || v.FECHA_VENTA <= fechaVentaFin.Value)
            .OrderByDescending(v => v.ACTIVO)
            .ThenByDescending(v => v.FECHA_REGISTRO)
            .Include(v => v.CLIENTE)
            .Include(v => v.USUARIO)
            .Include(v => v.DETALLEVENTA)

            .SelectMany(v => v.DETALLEVENTA.Select(d => new ConsultaDetalleVentaByIdVenta
            {
                IdProducto = d.ID_PRODUCTO,
                IdVenta = v.ID,
                NumeroVenta = v.NUMERO_VENTA,
                IdCliente = v.CLIENTE.ID,
                NombreCliente = v.CLIENTE.NOMBRES,
                Direccion = v.CLIENTE.DIRECCION,
                UrlBoletaFactura = v.URLBOLETAFACTURA,
                FechaVenta = v.FECHA_VENTA,
                UrlFotoProducto = d.URLFOTO_PRODUCTO,
                NombreProducto = d.NOMBRE_PRODUCTO,
                ColorProducto = d.COLOR_PRODUCTO,
                NombreCategoria = d.NOMBRE_CATEGORIA,
                ColorCategoria = d.COLOR_CATEGORIA,
                NombreMarca = d.NOMBRE_MARCA,
                ColorMarca = d.COLOR_MARCA,
                Cantidad = d.CANTIDAD,
                PrecioProducto = d.PRECIO_VENTA,
                PrecioTotal = d.PRECIO_TOTAL,
                NotaAdicional = v.NOTA_ADICIONAL
            }))
            .ToListAsync();
        }


        public async Task<List<ConsultaTotalCategorias>> SelectCategoriasTotalsByDateAsync(DateTime? fechaVentaInicio, DateTime? fechaVentaFin, int[] lstCategorias)
        {
            var totalCategoriaQuery = await All<DetalleVenta>()
                .AsNoTracking()
                .Where(
                    v => (!fechaVentaInicio.HasValue || v.FECHA_REGISTRO.Date >= fechaVentaInicio)
                    && (!fechaVentaFin.HasValue || v.FECHA_REGISTRO.Date <= fechaVentaFin)
                    && v.ACTIVO == Flags.Activado
                    && v.ESTADO == Flags.Habilitado
                    && v.VENTA.ESTADO == Flags.Habilitado
                    && v.VENTA.ACTIVO == Flags.Activado
                    && (lstCategorias.Length == Numeracion.Cero || lstCategorias.Contains(v.PRODUCTO.CATEGORIA.ID)))
                .GroupBy(i => new { i.NOMBRE_CATEGORIA, i.COLOR_CATEGORIA })
                .Select(i => new ConsultaTotalCategorias()
                {
                    NombreCategoria = i.Key.NOMBRE_CATEGORIA,
                    ColorCategoria = i.Key.COLOR_CATEGORIA,
                    TotalVenta = i.Sum(x => x.PRECIO_TOTAL)
                }).ToListAsync();

            return totalCategoriaQuery;
        }

        public async Task<List<ConsultaTotalMarcas>> SelectMarcasTotalsByDateAsync(DateTime? fechaVentaInicio, DateTime? fechaVentaFin, int[] lstMarcas)
        {
            var totalMarcasQuery = await All<DetalleVenta>()
                .AsNoTracking()
                .Where(
                    v => (!fechaVentaInicio.HasValue || v.FECHA_REGISTRO.Date >= fechaVentaInicio)
                    && (!fechaVentaFin.HasValue || v.FECHA_REGISTRO.Date <= fechaVentaFin)
                    && v.ESTADO == Flags.Habilitado
                    && v.ACTIVO == Flags.Activado
                    && v.VENTA.ESTADO == Flags.Habilitado
                    && v.VENTA.ACTIVO == Flags.Activado
                    && (lstMarcas.Length == Numeracion.Cero || lstMarcas.Contains(v.PRODUCTO.MARCA.ID))
                    )
                .GroupBy(i => new { i.NOMBRE_MARCA, i.COLOR_MARCA })
                .Select(i => new ConsultaTotalMarcas()
                {
                    NombreMarca = i.Key.NOMBRE_MARCA,
                    ColorMarca = i.Key.COLOR_MARCA,
                    TotalVenta = i.Sum(x => x.PRECIO_TOTAL)
                }).ToListAsync();

            return totalMarcasQuery;
        }

        public async Task<List<ConsultaTotalProductos>> SelectProductosTotalsByDateAsync(DateTime? fechaVentaInicio, DateTime? fechaVentaFin, int[] lstProductos)
        {
            var totalProductosQuery = await All<DetalleVenta>()
                .AsNoTracking()
                .Where(
                v => (!fechaVentaInicio.HasValue || v.FECHA_REGISTRO.Date >= fechaVentaInicio)
                && (!fechaVentaFin.HasValue || v.FECHA_REGISTRO.Date <= fechaVentaFin)
                && v.ESTADO == Flags.Habilitado
                && v.ACTIVO == Flags.Activado
                && v.VENTA.ESTADO == Flags.Habilitado
                && v.VENTA.ACTIVO == Flags.Activado
                && (lstProductos.Length == Numeracion.Cero || lstProductos.Contains(v.PRODUCTO.ID)))
                .GroupBy(i => new { i.NOMBRE_PRODUCTO, i.COLOR_PRODUCTO })
                .Select(i => new ConsultaTotalProductos()
                {
                    NombreProducto = i.Key.NOMBRE_PRODUCTO,
                    ColorProducto = i.Key.COLOR_PRODUCTO,
                    TotalVenta = i.Sum(x => x.PRECIO_TOTAL)
                }).ToListAsync();

            return totalProductosQuery;
        }

        public async Task<List<DetalleVenta>> InsertAsync(List<DetalleVenta> detalleVenta)
        {
            await base.InsertAsync(detalleVenta);
            return detalleVenta;
        }
    }
}
