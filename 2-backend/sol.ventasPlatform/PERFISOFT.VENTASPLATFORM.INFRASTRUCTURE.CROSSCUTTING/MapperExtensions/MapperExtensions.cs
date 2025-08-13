using AutoMapper;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Cliente.Filtro;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Menu.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Producto.Filtro;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Filtro;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Consulta;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Venta.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Medida.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Parametro.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Response;
using System.Globalization;
using DR = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.MapperExtensions
{
    public class MapperExtensions : Profile
    {
        public MapperExtensions()
        {
            #region DTO => ENT

            #region USUARIO

            CreateMap<RegistrarUsuarioRequest, Usuario>().AfterMap((src, dst) =>
            {
                dst.NOMBRES = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Nombres.ToLower()).Trim();
                dst.APELLIDOS = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Apellidos.ToLower()).Trim();
                dst.ID_ROL = src.IdRol;
                dst.ID_GENERO = src.IdGenero;
                dst.ID_TIPO_DOCUMENTO = src.IdTipoDocumento;
                dst.NUMERO_DOCUMENTO = src.NumeroDocumento.Trim();
                dst.CORREO_ELECTRONICO = src.Correo_Electronico.Trim();
                dst.FECHA_NACIMIENTO = src.Fecha_Nacimiento;
                dst.CELULAR = src.Celular.Trim();
                dst.DIRECCION = src.Direccion.Trim();
                dst.FLG_CAMBIAR_CLAVE = Flags.NuevoUsuario;
                dst.ACTIVO = Flags.Activar;
                dst.ESTADO = Flags.Habilitar;
                dst.FLG_PRIMERA_VEZ_LOGUEO = Flags.True;
                dst.FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(src.DestinationTimeZoneIdRegistro);
            });

            CreateMap<ObtenerUsuarioRequest, FiltroConsultaUsuario>().AfterMap((src, dst) =>
            {
                dst.LST_GENERO = src.LstGenero;
                dst.FECHA_REGISTRO_INICIO = src.FechaRegistroInicio?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.FECHA_REGISTRO_FIN = src.FechaRegistroFin?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.NOMBRES = src.Nombres;
                dst.APELLIDOS = src.Apellidos;
            });

            #endregion

            #region PRODUCTO

            CreateMap<RegistrarProductoRequest, Producto>().AfterMap((src, dst) =>
            {
                dst.ID_CATEGORIA = src.Id_Categoria;
                dst.ID_MARCA = src.Id_Marca;
                dst.CODIGO = src.Codigo?.Trim();
                dst.NOMBRE = src.Nombre?.Trim();
                dst.DESCRIPCION = src.Descripcion?.Trim();
                dst.COLOR = src.Color?.Trim();  
                dst.PRECIO_COMPRA = src.PrecioCompra;
                dst.PRECIO_VENTA = src.PrecioVenta;
                dst.STOCK = src.Stock;
                dst.ID_FOTO = src.IdFoto?.Trim();
                dst.URLFOTO = src.UrlFoto?.Trim();
                dst.ESTADO = Flags.Habilitar;
                dst.ACTIVO = Flags.Activar;
                dst.FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(src.DestinationTimeZoneIdRegistro);
            });

            CreateMap<ObtenerProductoRequest, FiltroConsultaProducto>().AfterMap((src, dst) =>
            {
                dst.LST_CATEGORIAS = src.LstCategorias;
                dst.LST_MARCAS = src.LstMarcas;
                dst.CODIGO = src.Codigo;
                dst.NOMBRE = src.Nombre;
                dst.FECHA_REGISTRO_INICIO = src.FechaRegistroInicio?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.FECHA_REGISTRO_FIN = src.FechaRegistroFin?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.PRECIO_COMPRA_INICIO = src.PrecioCompraInicio;
                dst.PRECIO_COMPRA_FIN = src.PrecioCompraFin;
                dst.PRECIO_VENTA_INICIO = src.PrecioVentaInicio;
                dst.PRECIO_VENTA_FIN = src.PrecioVentaFin;
            });



            #endregion

            #region CLIENTE

            CreateMap<RegistrarClienteRequest, Cliente>().AfterMap((src, dst) =>
            {
                dst.ID_TIPO_DOCUMENTO = src.IdTipoDocumento;
                dst.ID_GENERO = src.IdGenero;
                dst.NUMERO_DOCUMENTO = src.NumeroDocumento.Trim();
                dst.NOMBRES = src.Nombres.Trim();
                dst.APELLIDOS = src.Apellidos.Trim();
                dst.CORREO_ELECTRONICO = src.CorreoElectronico.Trim();
                dst.CELULAR = src.Celular.Trim();
                dst.DIRECCION = src.Direccion.Trim();
                dst.ESTADO = Flags.Habilitar;
                dst.ACTIVO = Flags.Activar;
                dst.FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(src.DestinationTimeZoneIdRegistro);
            });

            CreateMap<ObtenerClienteRequest, FiltroConsultaCliente>().AfterMap((src, dst) =>
            {
                dst.LST_TIPODOCUMENTO = src.LstTipoDocumento;
                dst.LST_GENERO = src.LstGenero;
                dst.NUMERO_DOCUMENTO = src.NumeroDocumento;
                dst.FECHA_REGISTRO_INICIO = src.FechaRegistroInicio?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.FECHA_REGISTRO_FIN = src.FechaRegistroFin?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.NOMBRES = src.Nombres;
                dst.APELLIDOS = src.Apellidos;
                dst.CELULAR = src.Celular;
                dst.DIRECCION = src.Direccion;
                dst.CORREO_ELECTRONICO = src.CorreoElectronico;
            });

            #endregion

            #region CATEGORÍA

            CreateMap<RegistrarCategoriaRequest, Categoria>().AfterMap((src, dst) =>
            {
                dst.ID_MEDIDA = src.Id_Medida;
                dst.NOMBRE = src.Nombre.Trim();
                dst.DESCRIPCION = src.Descripcion.Trim();
                dst.COLOR = src.Color.Trim();
                dst.ESTADO = Flags.Habilitar;
                dst.ACTIVO = Flags.Activar;
                dst.FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(src.DestinationTimeZoneIdRegistro);
            });

            #endregion

            #region MARCA

            CreateMap<RegistrarMarcaRequest, Marca>().AfterMap((src, dst) =>
            {
                dst.NOMBRE = src.Nombre.Trim();
                dst.DESCRIPCION = src.Descripcion.Trim();
                dst.COLOR = src.Color.Trim();
                dst.ESTADO = Flags.Habilitar;
                dst.ACTIVO = Flags.Activar;
                dst.FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(src.DestinationTimeZoneIdRegistro);
            });

            #endregion

            #region VENTA

            CreateMap<ObtenerVentaRequest, FiltroObtenerVenta>().AfterMap((src, dst) =>
            {
                dst.LstUsuario = src.LstUsuario;
                dst.NumeroVenta = src.NumeroVenta;
                dst.FechaVentaInicio = src.FechaVentaInicio?.ConvertDateTimeClient(src.DestinationTimeZoneId); ;
                dst.FechaVentaFin = src.FechaVentaFin?.ConvertDateTimeClient(src.DestinationTimeZoneId); ;
                dst.MontoVentaInicio = src.MontoVentaInicio;
                dst.MontoVentaFin = src.MontoVentaFin;
            });

            #region ANÁLISIS

            CreateMap<ObtenerReporteCategoriaRequest, FiltroDetalleVentaCategoriaReporte>().AfterMap((src, dst) =>
            {
                dst.LstCategorias = src.LstCategorias;
                dst.FechaVentaInicio = src.FechaVentaInicio?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.FechaVentaFin = src.FechaVentaFin?.ConvertDateTimeClient(src.DestinationTimeZoneId);
            });

            CreateMap<ObtenerReporteProductoRequest, FiltroDetalleVentaProductoReporte>().AfterMap((src, dst) =>
            {
                dst.LstProductos = src.LstProductos;
                dst.FechaVentaInicio = src.FechaVentaInicio?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.FechaVentaFin = src.FechaVentaFin?.ConvertDateTimeClient(src.DestinationTimeZoneId);
            });

            CreateMap<ObtenerReporteMarcaRequest, FiltroDetalleVentaMarcaReporte>().AfterMap((src, dst) =>
            {
                dst.LstMarcas = src.LstMarcas;
                dst.FechaVentaInicio = src.FechaVentaInicio?.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.FechaVentaFin = src.FechaVentaFin?.ConvertDateTimeClient(src.DestinationTimeZoneId);
            });

            CreateMap<ObtenerResumenReporteRequest, FiltroResumenReporte>().AfterMap((src, dst) =>
            {
                dst.FechaVentaInicio = src.FechaInicio.ConvertDateTimeClient(src.DestinationTimeZoneId);
                dst.FechaVentaFin = src.FechaFin.ConvertDateTimeClient(src.DestinationTimeZoneId);
            });

            #endregion

            CreateMap<FiltroRegistrarVenta, Venta>().AfterMap((src, dst) =>
            {
                dst.ID_USUARIO = src.IdUsuario;
                dst.ID_CLIENTE = src.IdCliente;
                dst.NUMERO_VENTA = src.NumeroVenta;
                dst.PRECIO_TOTAL = src.PrecioTotal;
                dst.FECHA_VENTA = src.FechaVenta;
                dst.NOTA_ADICIONAL = src.NotaAdicional;
                dst.ESTADO = src.Estado;
                dst.ACTIVO = src.Activo;
                dst.FECHA_REGISTRO = src.FechaRegistro;
            });

            CreateMap<FiltroRegistrarDetalleVenta, DetalleVenta>().AfterMap((src, dst) =>
            {
                dst.ID_VENTA = src.IdVenta;
                dst.ID_PRODUCTO = src.IdProducto;
                dst.NOMBRE_PRODUCTO = src.NombreProducto;
                dst.COLOR_PRODUCTO = src.ColorProducto;
                dst.NOMBRE_CATEGORIA = src.NombreCategoria;
                dst.COLOR_CATEGORIA = src.ColorCategoria;
                dst.NOMBRE_MARCA = src.NombreMarca;
                dst.COLOR_MARCA = src.ColorMarca;
                dst.CANTIDAD = src.Cantidad;
                dst.PRECIO_COMPRA = src.PrecioCompra;
                dst.PRECIO_VENTA = src.PrecioVenta;
                dst.PRECIO_TOTAL = src.PrecioTotal;
                dst.ESTADO = src.Estado;
                dst.ACTIVO = src.Activo;
                dst.FECHA_REGISTRO = src.FechaRegistro;
                dst.URLFOTO_PRODUCTO = src.UrlFotoProducto;
            });

            #endregion

            #endregion

            #region ENT => DTO

            #region USUARIO

            CreateMap<Usuario, UsuarioDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<UsuarioId, UsuarioIdDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ObtenerColaboradoresActivos, ObtenerColaboradoresActivosDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<CategoriaConConteoVO, CategoriaConConteoDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            #endregion

            #region MONEDA

            CreateMap<Moneda, MonedaDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            #endregion

            #region CATEGORIA

            CreateMap<Categoria, CategoriaDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Categoria, ResponseDTO>().AfterMap((src, dst) =>
            {
                dst.Success = Flags.SuccessTransaction;
                dst.TitleMessage = DR.Dictionary.SuccessTitleTransaction;
                dst.Message = DR.Dictionary.SuccessTransaction;
                dst.Id = src.ID;
            });

            #endregion

            #region PRODUCTO

            CreateMap<Producto, ProductoDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Producto, ResponseDTO>().AfterMap((src, dst) =>
            {
                dst.Success = Flags.SuccessTransaction;
                dst.TitleMessage = DR.Dictionary.SuccessTitleTransaction;
                dst.Message = DR.Dictionary.SuccessTransaction;
                dst.Id = src.ID;
            });
            #endregion

            #region MARCA

            CreateMap<Marca, MarcaDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Marca, ResponseDTO>().AfterMap((src, dst) =>
            {
                dst.Success = Flags.SuccessTransaction;
                dst.TitleMessage = DR.Dictionary.SuccessTitleTransaction;
                dst.Message = DR.Dictionary.SuccessTransaction;
                dst.Id = src.ID;
            });
            #endregion

            #region CLIENTE

            CreateMap<Cliente, ClienteDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Cliente, ResponseDTO>().AfterMap((src, dst) =>
            {
                dst.Success = Flags.SuccessTransaction;
                dst.TitleMessage = DR.Dictionary.SuccessTitleTransaction;
                dst.Message = DR.Dictionary.SuccessTransaction;
                dst.Id = src.ID;
            });

            #endregion

            #region MEDIDA

            CreateMap<Medida, MedidaDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            #endregion

            #region MENUROL

            CreateMap<ObtenerMenuRol, MenuRolDTO>().AfterMap((src, dst) =>
            {
                dst.Padre_texto = src.PADRE_TEXTO;
                dst.Titulo = src.TITULO;
                dst.Tipo = src.TIPO;
                dst.Icono = src.ICONO;
                dst.Flg_enlace_externo = src.FLG_ENLACE_EXTERNO;
                dst.Flg_menu_hijo = src.FLG_MENU_HIJO;
                dst.Ruta = src.RUTA;
                dst.Orden = src.ORDEN;
            });

            #endregion

            #region ROL

            CreateMap<Rol, RolDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            #endregion

            #region MONEDA

            CreateMap<Moneda, MonedaDTO>().AfterMap((src, dst) =>
            {
                dst.Region_iso_dos_letras = src.REGION_ISO_DOS_LETRAS;
                dst.Region_iso_tres_letras = src.REGION_ISO_TRES_LETRAS;
                dst.Codigo_moneda = src.CODIGO_MONEDA;
                dst.Lenguaje_codigo = src.LENGUAJE_CODIGO;
                dst.Lenguaje_descripcion = src.LENGUAJE_DESCRIPCION;
                dst.Cultereinfo = src.CULTUREINFO;
                dst.Pais = src.PAIS;
                dst.Descripcion = src.DESCRIPCION;
                dst.Simbolo = src.SIMBOLO;
            });

            #endregion

            #region TIPODOCUMENTO

            CreateMap<TipoDocumento, TipoDocumentoDTO>().AfterMap((src, dst) =>
            {
                dst.Codigo = src.CODIGO;
                dst.descripcion = src.DESCRIPCION;
                dst.Orden = src.ORDEN;
                dst.Estado = src.ESTADO;
                dst.Motivo_anulacion = src.MOTIVO_ANULACION;
                dst.Fecha_registro = src.FECHA_REGISTRO;
                dst.Fecha_actualizacion = src.FECHA_ACTUALIZACION;
                dst.Fecha_anulacion = src.FECHA_ANULACION;
            });

            #endregion

            #region VENTA

            CreateMap<ConsultaVentaByFilter, VentaDTO>().AfterMap((src, dst) =>
            {
                dst.IdVenta = src.IdVenta;
                dst.NumeroVenta = src.NumeroVenta;
                dst.NombreCliente = src.NombreCliente;
                dst.CorreoUsuario = src.CorreoUsuario;
                dst.FechaVenta = src.FechaVenta;
                dst.TotalVenta = src.TotalVenta;
                dst.UrlBoletaFactura = src.UrlBoletaFactura;
                dst.Estado = src.Estado;
            });

            CreateMap<ConsultaDetalleVentaByIdVenta, DetalleVentaDTO>().AfterMap((src, dst) =>
            {
                dst.UrlFotoProducto = src.UrlFotoProducto;
                dst.NombreProducto = src.NombreProducto;
                dst.ColorProducto = src.ColorProducto;
                dst.NombreCategoria = src.NombreCategoria;
                dst.ColorCategoria = src.ColorCategoria;
                dst.NombreMarca = src.NombreMarca;
                dst.ColorMarca = src.ColorMarca;
                dst.PrecioProducto = src.PrecioProducto;
                dst.Cantidad = src.Cantidad;
                dst.PrecioTotal = src.PrecioTotal;
            });

            #endregion

            #region GENERO

            CreateMap<Genero, GeneroDTO>().AfterMap((src, dst) =>
            {
                dst.Codigo = src.CODIGO;
                dst.Descripcion = src.DESCRIPCION;
                dst.Orden = src.ORDEN;
                dst.Estado = src.ESTADO;
                dst.Motivo_anulacion = src.MOTIVO_ANULACION;
                dst.Fecha_registro = src.FECHA_REGISTRO;
                dst.Fecha_actualizacion = src.FECHA_ACTUALIZACION;
                dst.Fecha_anulacion = src.FECHA_ANULACION;
            });

            #endregion

            #region NEGOCIO

            CreateMap<Negocio, NegocioDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            #endregion

            #region Parametro

            CreateMap<Parametro, ParametroDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ParametroDetalle, ParametroDetalleDTO>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            #endregion

            #endregion
        }

    }
}
