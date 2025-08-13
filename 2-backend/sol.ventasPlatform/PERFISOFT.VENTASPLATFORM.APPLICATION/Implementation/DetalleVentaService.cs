using AutoMapper;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Categoria;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Marca;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response.Reporte.Producto;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Categorias;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Marcas;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Productos;
namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class DetalleVentaService : IDetalleVentaService
    {
        private readonly IMapper _mapper;
        private readonly IVentaRepository _ventaRepository;
        private readonly IDetalleVentaRepository _detalleVentaRepository;
        private readonly ICategoriasInfrastructureService _categoriasInfrastructureService;
        private readonly IMarcasInfrastructureService _marcasInfrastructureService;
        private readonly IProductosInfrastructureService _productosInfrastructureService;
        private readonly IParametrosGeneralesRepository _parametrosGeneralesRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProductoRepository _productoRepository;

        public DetalleVentaService(IMapper mapper,
                                   IDetalleVentaRepository detalleVentaRepository,
                                   ICategoriasInfrastructureService categoriasInfrastructureService,
                                   IMarcasInfrastructureService marcasInfrastructureService,
                                   IProductosInfrastructureService productosInfrastructureService,
                                   IParametrosGeneralesRepository parametrosGeneralesRepository,
                                   IVentaRepository ventaRepository,
                                   IClienteRepository clienteRepository,
                                   IProductoRepository productoRepository
                                   )
        {
            _mapper = mapper;
            _detalleVentaRepository = detalleVentaRepository;
            _parametrosGeneralesRepository = parametrosGeneralesRepository;
            _categoriasInfrastructureService = categoriasInfrastructureService;
            _marcasInfrastructureService = marcasInfrastructureService;
            _productosInfrastructureService = productosInfrastructureService;
            _clienteRepository = clienteRepository;
            _productoRepository = productoRepository;
            _ventaRepository = ventaRepository;

        }

        public async Task<IList<DetalleVentaDTO>> GetDetalleAsync(ObtenerDetalleVentaRequest request)
        {
            return _mapper.Map<IList<DetalleVentaDTO>>(await _detalleVentaRepository.SelectDetalleAsync(request.IdVenta, 
                                                                                                        request.LstUsuario, 
                                                                                                        request.NumeroVenta, 
                                                                                                        request.FechaVentaInicio?.ConvertDateTimeClient(request.DestinationTimeZoneId), 
                                                                                                        request.FechaVentaFin?.ConvertDateTimeClient(request.DestinationTimeZoneId)));
        }

        public async Task<VentaAnalisisProductosDTO> GetAnalisisProductosByFilterAsync(ObtenerReporteProductoRequest request)
        {
            var filtro = _mapper.Map<FiltroDetalleVentaProductoReporte>(request);

            var dataVentas = await _detalleVentaRepository.SelectAllForReportsByFilterAsync(filtro);

            var distribucionVentasProductos = ObtenerDistribucionVentasProductos(dataVentas);

            var ventasAgrupados = AgruparProductosFecha(dataVentas);

            var lstEvolucionVentasProductoFecha = GenerarVentasAnalisisProductosAgrupado(ventasAgrupados);

            var evolucionVentasFecha = ObtenerVentasTotalesFecha(dataVentas);

            var topDiezVentas = ObtenerTopDiezProductos(dataVentas);

            return new VentaAnalisisProductosDTO()
            {
                LstEvolucionVentasProductoFecha = lstEvolucionVentasProductoFecha,
                EvolucionVentasFecha = evolucionVentasFecha,
                DistribucionVentasProducto = distribucionVentasProductos,
                TopDiezProductosVentas = topDiezVentas,
            };
        }

        public async Task<VentaAnalisisCategoriasDTO> GetAnalisisCategoriasByFilterAsync(ObtenerReporteCategoriaRequest request)
        {
            var filtro = _mapper.Map<FiltroDetalleVentaCategoriaReporte>(request);

            var dataVentas = await _detalleVentaRepository.SelectAllForReportsByFilterAsync(filtro);

            var distribucionVentasCategorias = ObtenerDistribucionVentasCategorias(dataVentas);

            var ventasAgrupados = AgruparProductosCategoriaFecha(dataVentas);

            var lstEvolucionVentasCategoriaFecha = GenerarVentasAnalisisCategoriasAgrupado(ventasAgrupados);

            var evolucionVentasFecha = ObtenerVentasTotalesFecha(dataVentas);

            var topDiezVentas = ObtenerTopDiezCategorias(dataVentas);

            return new VentaAnalisisCategoriasDTO()
            {
                LstEvolucionVentasCategoriaFecha = lstEvolucionVentasCategoriaFecha,
                EvolucionVentasFecha = evolucionVentasFecha,
                DistribucionVentasCategoria = distribucionVentasCategorias,
                TopDiezCategoriasVentas = topDiezVentas,
            };
        }

        public async Task<VentaAnalisisMarcasDTO> GetAnalisisMarcasByFilterAsync(ObtenerReporteMarcaRequest request)
        {
            var filtro = _mapper.Map<FiltroDetalleVentaMarcaReporte>(request);

            var dataVentas = await _detalleVentaRepository.SelectAllForReportsByFilterAsync(filtro);

            var distribucionVentasMarcas = ObtenerDistribucionVentasMarcas(dataVentas);

            var ventasAgrupados = AgruparProductosMarcasFecha(dataVentas);

            var lstEvolucionVentasMarcasFecha = GenerarVentasAnalisisMarcasAgrupado(ventasAgrupados);

            var evolucionVentasFecha = ObtenerVentasTotalesFecha(dataVentas);

            var topDiezVentas = ObtenerTopDiezMarcas(dataVentas);

            return new VentaAnalisisMarcasDTO()
            {
                LstEvolucionVentasMarcaFecha = lstEvolucionVentasMarcasFecha,
                EvolucionVentasFecha = evolucionVentasFecha,
                DistribucionVentasMarca = distribucionVentasMarcas,
                TopDiezMarcasVentas = topDiezVentas,
            };
        }

        public async Task<ReporteResumenDTO> GetResumenReporteAsync(ObtenerResumenReporteRequest request)
        {
            var fechaInicioConverted = request.FechaInicio.ConvertDateTimeClient(request.DestinationTimeZoneId);

            var fechaFinConverted = request.FechaFin.ConvertDateTimeClient(request.DestinationTimeZoneId);

            var totalProductosRegistrados = await _productoRepository.CountByFechaRegistroAsync(fechaInicioConverted, fechaFinConverted);

            var totalClientesRegistrados = await _clienteRepository.CountByFechaRegistroAsync(fechaInicioConverted, fechaFinConverted);

            var totalVentasRegistradas = await _ventaRepository.SelectTotalVentasAsync(fechaInicioConverted, fechaFinConverted);

            var totalVentasAnuladas = await _ventaRepository.SelectTotalVentasAnuladasAsync(fechaInicioConverted, fechaFinConverted);

            var dataVentas = await _detalleVentaRepository.SelectAllForReportsByFilterAsync(_mapper.Map<FiltroResumenReporte>(request));

            var evolucionVentasFecha = ObtenerVentasTotalesFecha(dataVentas);

            var topDiezMarcas = ObtenerTopDiezMarcas(dataVentas);

            var topDiezProductos = ObtenerTopDiezProductos(dataVentas);

            var distribucionVentasProductos = ObtenerDistribucionVentasProductos(dataVentas);

            var distribucionVentasMarcas = ObtenerDistribucionVentasMarcas(dataVentas);

            return new ReporteResumenDTO()
            {
                EvolucionVentasFecha = evolucionVentasFecha,
                TopDiezMarcasVentas = topDiezMarcas,
                TopDiezProductosVentas = topDiezProductos,
                DistribucionVentasProductos = distribucionVentasProductos,
                DistribucionVentasMarca = distribucionVentasMarcas,
                TotalVentasRegistradas = totalVentasRegistradas,
                TotalVentasAnuladas = totalVentasAnuladas,
                TotalProductosRegistrados = totalProductosRegistrados,
                TotalClientesRegistrados = totalClientesRegistrados
            };
        }

        public async Task<ResponseDTO> GetReportePorCategoriasAsync(ObtenerReporteCategoriaRequest request)
        {
            var filtro = _mapper.Map<FiltroDetalleVentaCategoriaReporte>(request);

            var dataDetalleVentas = await _detalleVentaRepository.SelectAllForReportsByFilterAsync(filtro);

            var totalVentasCategorias = await _detalleVentaRepository.SelectCategoriasTotalsByDateAsync(filtro.FechaVentaInicio, filtro.FechaVentaFin, filtro.LstCategorias);

            var idMonedaNegocio = await _parametrosGeneralesRepository.GetIdMonedaNegocioAsync();

            var monedaNegocio = await _parametrosGeneralesRepository.GetMonedaByIdAsync(idMonedaNegocio);

            var filtroReporte = new FiltroReportePorCategorias();

            filtroReporte.LstDetalleVenta = dataDetalleVentas;
            filtroReporte.LstDetalleTotalCategorias = totalVentasCategorias;
            filtroReporte.FechaDesde = filtro.FechaVentaInicio;
            filtroReporte.FechaHasta = filtro.FechaVentaFin;
            filtroReporte.CodigoMoneda = monedaNegocio.CODIGO_MONEDA;

            var reportePorCategoria = await _categoriasInfrastructureService.GenerarReportePorCategoriasAsync(filtroReporte);

            return new ResponseDTO
            {
                Success = Flags.SuccessTransaction,
                ArrayValue = reportePorCategoria
            };
        }

        public async Task<ResponseDTO> GetReportePorMarcasAsync(ObtenerReporteMarcaRequest request)
        {
            var filtro = _mapper.Map<FiltroDetalleVentaMarcaReporte>(request);

            var dataDetalleVentas = await _detalleVentaRepository.SelectAllForReportsByFilterAsync(filtro);

            var totalVentasMarcas = await _detalleVentaRepository.SelectMarcasTotalsByDateAsync(filtro.FechaVentaInicio, filtro.FechaVentaFin, filtro.LstMarcas);

            var idMonedaNegocio = await _parametrosGeneralesRepository.GetIdMonedaNegocioAsync();

            var monedaNegocio = await _parametrosGeneralesRepository.GetMonedaByIdAsync(idMonedaNegocio);

            var filtroReporte = new FiltroReportePorMarcas();

            filtroReporte.LstDetalleVenta = dataDetalleVentas;
            filtroReporte.LstDetalleTotalMarcas = totalVentasMarcas;
            filtroReporte.FechaDesde = filtro.FechaVentaInicio;
            filtroReporte.FechaHasta = filtro.FechaVentaFin;
            filtroReporte.CodigoMoneda = monedaNegocio.CODIGO_MONEDA;

            var reportePorMarca = await _marcasInfrastructureService.GenerarReportePorMarcasAsync(filtroReporte);

            return new ResponseDTO
            {
                Success = Flags.SuccessTransaction,
                ArrayValue = reportePorMarca
            };
        }

        public async Task<ResponseDTO> GetReportePorProductosAsync(ObtenerReporteProductoRequest request)
        {
            var filtro = _mapper.Map<FiltroDetalleVentaProductoReporte>(request);

            var dataDetalleVentas = await _detalleVentaRepository.SelectAllForReportsByFilterAsync(filtro);

            var totalVentasProductos = await _detalleVentaRepository.SelectProductosTotalsByDateAsync(filtro.FechaVentaInicio, filtro.FechaVentaFin, filtro.LstProductos);

            var idMonedaNegocio = await _parametrosGeneralesRepository.GetIdMonedaNegocioAsync();

            var monedaNegocio = await _parametrosGeneralesRepository.GetMonedaByIdAsync(idMonedaNegocio);

            var filtroReporte = new FiltroReportePorProductos();

            filtroReporte.LstDetalleVenta = dataDetalleVentas;
            filtroReporte.LstDetalleTotalProductos = totalVentasProductos;
            filtroReporte.FechaDesde = filtro.FechaVentaInicio;
            filtroReporte.FechaHasta = filtro.FechaVentaFin;
            filtroReporte.CodigoMoneda = monedaNegocio.CODIGO_MONEDA;

            var reportePorProducto = await _productosInfrastructureService.GenerarReportePorProductosAsync(filtroReporte);

            return new ResponseDTO
            {
                Success = Flags.SuccessTransaction,
                ArrayValue = reportePorProducto
            };
        }

        #region PRIVATE METHODS

        private List<VentaAnalisisProductosAgrupadosDTO> AgruparProductosFecha(List<DetalleVenta> detalleVentas)
        {
            return detalleVentas
                .GroupBy(i => new { i.NOMBRE_PRODUCTO, i.COLOR_PRODUCTO, i.FECHA_REGISTRO.Date })
                .Select(g => new VentaAnalisisProductosAgrupadosDTO
                {
                    NombreProducto = g.First().NOMBRE_PRODUCTO,
                    ColorProducto = g.First().COLOR_PRODUCTO,
                    FechaVentaAgrupada = g.Key.Date,
                    MontoTotal = g.Sum(i => i.PRECIO_TOTAL)
                })
                .ToList();
        }

        private List<EvolucionVentasProductosFechaDTO> GenerarVentasAnalisisProductosAgrupado(List<VentaAnalisisProductosAgrupadosDTO> ventasAgrupados)
        {
            return ventasAgrupados
               .GroupBy(i => new { i.NombreProducto, i.ColorProducto })
                .Select(grupo => new EvolucionVentasProductosFechaDTO
                {
                    NombreProducto = grupo.First().NombreProducto,
                    ColorProducto = grupo.First().ColorProducto,
                    DatosVentasAgrupados = grupo.Select(i => new VentasAnalisisAgrupadosProductosDTO
                    {
                        FechaVenta = i.FechaVentaAgrupada,
                        MontoVentaTotal = i.MontoTotal
                    }).ToList()
                })
                .ToList();
        }

        private DistribucionVentasProductosDTO ObtenerDistribucionVentasProductos(List<DetalleVenta> lstDetalleVentas)
        {
            var totalVentasProductos = lstDetalleVentas.GroupBy(i => new { i.NOMBRE_PRODUCTO, i.COLOR_PRODUCTO })
                .Select(i => new ConsultaTotalVentasProductos()
                {
                    NombreProducto = i.Key.NOMBRE_PRODUCTO,
                    ColorProducto = i.Key.COLOR_PRODUCTO,
                    TotalVenta = i.Sum(x => x.PRECIO_TOTAL)
                }).ToList();

            return new DistribucionVentasProductosDTO()
            {
                ColoresProductos = totalVentasProductos?.Select(item => item.ColorProducto).ToArray(),
                NombreProductos = totalVentasProductos?.Select(item => item.NombreProducto).ToArray(),
                TotalVentasProductos = totalVentasProductos?.Select(item => item.TotalVenta).ToArray()
            };

        }

        private VentasProductosTopDiezDTO ObtenerTopDiezProductos(List<DetalleVenta> lstDetalleVentas)
        {
            var ventasAgrupados = lstDetalleVentas
              .GroupBy(i => new { i.NOMBRE_PRODUCTO, i.COLOR_PRODUCTO })
              .Select(grupo => new
              {
                  Nombre = grupo.Key.NOMBRE_PRODUCTO,
                  MontoTotal = grupo.Sum(g => g.PRECIO_TOTAL),
                  Color = grupo.Key.COLOR_PRODUCTO
              })
              .OrderByDescending(dto => dto.MontoTotal)
              .Take(Numeracion.Diez)
              .ToList();

            var productos = ventasAgrupados?.Select(item => item.Nombre).ToArray();
            var colores = ventasAgrupados?.Select(item => item.Color).ToArray();
            var montos = ventasAgrupados.Select(item => item.MontoTotal).ToArray();

            return new VentasProductosTopDiezDTO()
            {
                Productos = productos,
                Color = colores,
                TotalMontos = montos,
            };
        }

        private List<VentaAnalisisCategoriasAgrupadosDTO> AgruparProductosCategoriaFecha(List<DetalleVenta> detalleVentas)
        {
            return detalleVentas
                .GroupBy(i => new { i.NOMBRE_CATEGORIA, i.COLOR_CATEGORIA, i.FECHA_REGISTRO.Date })
                .Select(g => new VentaAnalisisCategoriasAgrupadosDTO
                {
                    NombreCategoria = g.First().NOMBRE_CATEGORIA,
                    ColorCategoria = g.First().COLOR_CATEGORIA,
                    FechaVentaAgrupada = g.Key.Date,
                    MontoTotal = g.Sum(i => i.PRECIO_TOTAL)
                })
                .ToList();
        }

        private List<EvolucionVentasCategoriaFechaDTO> GenerarVentasAnalisisCategoriasAgrupado(List<VentaAnalisisCategoriasAgrupadosDTO> ventasAgrupados)
        {
            return ventasAgrupados
      .GroupBy(i => new { i.NombreCategoria, i.ColorCategoria })
                .Select(grupo => new EvolucionVentasCategoriaFechaDTO
                {
                    NombreCategoria = grupo.First().NombreCategoria,
                    ColorCategoria = grupo.First().ColorCategoria,
                    DatosVentasAgrupados = grupo.Select(i => new VentasAnalisisAgrupadosCategoriasDTO
                    {
                        FechaVenta = i.FechaVentaAgrupada,
                        MontoVentaTotal = i.MontoTotal
                    }).ToList()
                })
                .ToList();
        }

        private DistribucionVentasCategoriaDTO ObtenerDistribucionVentasCategorias(List<DetalleVenta> lstDetalleVentas)
        {
            var totalVentasCategorias = lstDetalleVentas.GroupBy(i => new { i.NOMBRE_CATEGORIA, i.COLOR_CATEGORIA })
                .Select(i => new ConsultaTotalVentasCategorias()
                {
                    NombreCategoria = i.Key.NOMBRE_CATEGORIA,
                    ColorCategoria = i.Key.COLOR_CATEGORIA,
                    TotalVenta = i.Sum(x => x.PRECIO_TOTAL)
                }).ToList();

            return new DistribucionVentasCategoriaDTO()
            {
                ColoresCategorias = totalVentasCategorias?.Select(item => item.ColorCategoria).ToArray(),
                NombreCategorias = totalVentasCategorias?.Select(item => item.NombreCategoria).ToArray(),
                TotalVentasCategorias = totalVentasCategorias?.Select(item => item.TotalVenta).ToArray()
            };

        }

        private VentasCategoriasTopDiezDTO ObtenerTopDiezCategorias(List<DetalleVenta> lstDetalleVentas)
        {
            var ventasAgrupados = lstDetalleVentas
              .GroupBy(i => new { i.NOMBRE_CATEGORIA, i.COLOR_CATEGORIA })
              .Select(grupo => new
              {
                  Nombre = grupo.Key.NOMBRE_CATEGORIA,
                  MontoTotal = grupo.Sum(g => g.PRECIO_TOTAL),
                  Color = grupo.Key.COLOR_CATEGORIA
              })
              .OrderByDescending(dto => dto.MontoTotal)
              .Take(Numeracion.Diez)
              .ToList();

            var categorias = ventasAgrupados?.Select(item => item.Nombre).ToArray();
            var colores = ventasAgrupados?.Select(item => item.Color).ToArray();
            var montos = ventasAgrupados.Select(item => item.MontoTotal).ToArray();

            return new VentasCategoriasTopDiezDTO()
            {
                Categorias = categorias,
                Color = colores,
                TotalMontos = montos,
            };
        }

        private List<VentaAnalisisMarcasAgrupadosDTO> AgruparProductosMarcasFecha(List<DetalleVenta> detalleVentas)
        {
            return detalleVentas
                .GroupBy(i => new { i.NOMBRE_MARCA, i.COLOR_MARCA, i.FECHA_REGISTRO.Date })
                .Select(g => new VentaAnalisisMarcasAgrupadosDTO
                {
                    NombreMarca = g.First().NOMBRE_MARCA,
                    ColorMarca = g.First().COLOR_MARCA,
                    FechaVentaAgrupada = g.Key.Date,
                    MontoTotal = g.Sum(i => i.PRECIO_TOTAL)
                })
                .ToList();
        }

        private List<EvolucionVentasMarcaFechaDTO> GenerarVentasAnalisisMarcasAgrupado(List<VentaAnalisisMarcasAgrupadosDTO> ventasAgrupados)
        {
            return ventasAgrupados
           .GroupBy(i => new { i.NombreMarca, i.ColorMarca })
                .Select(grupo => new EvolucionVentasMarcaFechaDTO
                {
                    NombreMarca = grupo.First().NombreMarca,
                    ColorMarca = grupo.First().ColorMarca,
                    DatosVentasAgrupados = grupo.Select(i => new VentasAnalisisAgrupadosMarcasDTO
                    {
                        FechaVenta = i.FechaVentaAgrupada,
                        MontoVentaTotal = i.MontoTotal
                    }).ToList()
                })
                .ToList();
        }

        private DistribucionVentasMarcasDTO ObtenerDistribucionVentasMarcas(List<DetalleVenta> lstDetalleVentas)
        {
            var totalVentasMarcas = lstDetalleVentas.GroupBy(i => new { i.NOMBRE_MARCA, i.COLOR_MARCA })
                .Select(i => new ConsultaTotalVentasMarcas()
                {
                    NombreMarca = i.Key.NOMBRE_MARCA,
                    ColorMarca = i.Key.COLOR_MARCA,
                    TotalVenta = i.Sum(x => x.PRECIO_TOTAL)
                }).ToList();

            return new DistribucionVentasMarcasDTO()
            {
                ColoresMarcas = totalVentasMarcas?.Select(item => item.ColorMarca).ToArray(),
                nombreMarcas = totalVentasMarcas?.Select(item => item.NombreMarca).ToArray(),
                TotalVentasMarcas = totalVentasMarcas?.Select(item => item.TotalVenta).ToArray()
            };
        }

        private VentasMarcasTopDiezDTO ObtenerTopDiezMarcas(List<DetalleVenta> lstDetalleVentas)
        {
            var ventasAgrupados = lstDetalleVentas
              .GroupBy(i => new { i.NOMBRE_MARCA, i.COLOR_MARCA })
              .Select(grupo => new
              {
                  Nombre = grupo.Key.NOMBRE_MARCA,
                  MontoTotal = grupo.Sum(g => g.PRECIO_TOTAL),
                  Color = grupo.Key.COLOR_MARCA
              })
              .OrderByDescending(dto => dto.MontoTotal)
              .Take(Numeracion.Diez)
              .ToList();

            var marcas = ventasAgrupados?.Select(item => item.Nombre).ToArray();
            var colores = ventasAgrupados?.Select(item => item.Color).ToArray();
            var montos = ventasAgrupados.Select(item => item.MontoTotal).ToArray();

            return new VentasMarcasTopDiezDTO()
            {
                Marcas = marcas,
                Color = colores,
                TotalMontos = montos,
            };
        }

        private EvolucionVentasFechaDTO ObtenerVentasTotalesFecha(List<DetalleVenta> lstDetalleVentas)
        {
            var totalVentas = lstDetalleVentas.GroupBy(i => new { i.FECHA_REGISTRO.Date })
                 .Select(i => new ConsultaTotalVentasDate()
                 {
                     FechaVenta = i.Key.Date,
                     TotalVenta = i.Sum(x => x.PRECIO_TOTAL)
                 })
                 .ToList();

            return new EvolucionVentasFechaDTO()
            {
                FechaVenta = totalVentas?.Select(item => item.FechaVenta).ToArray(),
                TotalVenta = totalVentas?.Select(item => item.TotalVenta).ToArray(),
            };
        }

        #endregion

    }
}
