using FluentValidation;
using PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Authentication;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Email;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.PasswordHashing;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Marca.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametroDetalle.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Validator;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Repository;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.UnitOfWork;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.QuestPDFLibrary;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Categorias;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Marcas;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.Reportes.Productos;
using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.API.Configuration
{
    public static class DependencyInjection
    {
        public static void SetInjection(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repository

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IParametrosGeneralesRepository, ParametrosGeneralesRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IMedidaRepository, MedidaRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IVentaNumeroCorrelativoRepository, VentaNumeroCorrelativoRepository>();
            services.AddScoped<IDetalleVentaRepository, DetalleVentaRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<IParametroRepository, ParametroRepository>();
            services.AddScoped<IParametroDetalleRepository, ParametroDetalleRepository>();

            #endregion

            #region Application

            services.AddScoped<IParametrosGeneralesService, ParametrosGeneralesService>();
            services.AddScoped<IInventarioService, InventarioService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDetalleVentaService, DetalleVentaService>();
            services.AddScoped<IParametroService, ParametroService>();

            #endregion

            #region InfraStructure

            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IVentaQuestService, VentaQuestService>();
            services.AddScoped<ICategoriasInfrastructureService, CategoriasInfrastructureService>();
            services.AddScoped<IMarcasInfrastructureService, MarcasInfrastructureService>();
            services.AddScoped<IProductosInfrastructureService, ProductosInfrastructureService>();

            #endregion

            #region Fluent Validations Request

            #region USUARIO
            services.AddScoped<IValidator<RegistrarUsuarioRequest>, RegistrarUsuarioValidator>();
            services.AddScoped<IValidator<ActualizarUsuarioRequest>, ActualizarUsuarioValidator>();
            services.AddScoped<IValidator<ActualizarActivoUsuarioRequest>, ActualizarActivoUsuarioValidator>();
            services.AddScoped<IValidator<EliminarUsuarioRequest>, EliminarUsuarioValidator>();
            services.AddScoped<IValidator<IniciaSesionRequest>, IniciaSesionValidator>();
            services.AddScoped<IValidator<ObtenerUsuarioRequest>, ObtenerUsuarioValidator>();
            services.AddScoped<IValidator<ExistCorreoClienteRequest>, ExistCorreoClienteValidator>();
            services.AddScoped<IValidator<ExistCorreoUsuarioRequest>, ExistCorreoUsuarioValidator>();
            services.AddScoped<IValidator<ExistNumeroDocumentoClienteRequest>, ExistNumeroDocumentoClienteValidator>();
            services.AddScoped<IValidator<ExistNumeroDocumentoUsuarioRequest>, ExistNumeroDocumentoUsuarioValidator>();
            services.AddScoped<IValidator<VerifyOTPEmailRequest>, VerifyOTPEmailValidator>();
            services.AddScoped<IValidator<GenerateOTPEmailRequest>, GenerateOTPEmailValidator>();
            services.AddScoped<IValidator<GenerateOTPEmailRequest>, GenerateOTPEmailValidator>();
            services.AddScoped<IValidator<NotifyOlvideContraseniaRequest>, NotifyOlvideContraseniaValidator>();
            services.AddScoped<IValidator<RestablecerContraseniaRequest>, RestablecerContraseniaValidator>();
            services.AddScoped<IValidator<ActualizarContraseniaRequest>, ActualizarContraseniaValidator>();
            services.AddScoped<IValidator<ActualizarDetalleUsuarioRequest>, ActualizarDetalleUsuarioValidator>();
            services.AddScoped<IValidator<ActualizarNoEsPrimerLogueoRequest>, ActualizarNoEsPrimerLogueoValidator>();
            services.AddScoped<IValidator<ObtenerColaboradoresRequest>, ObtenerColaboradoresValidator>();

            #endregion

            #region NEGOCIO
            services.AddScoped<IValidator<ActualizarNegocioRequest>, ActualizarNegocioValidator>();
            #endregion

            #region INVENTARIO

            #region CATEGORIA

            services.AddScoped<IValidator<RegistrarCategoriaRequest>, RegistrarCategoriaValidator>();
            services.AddScoped<IValidator<ActualizarCategoriaRequest>, ActualizarCategoriaValidator>();
            services.AddScoped<IValidator<ActualizarActivoCategoriaRequest>, ActualizarActivoCategoriaValidator>();
            services.AddScoped<IValidator<ObtenerCategoriaRequest>, ObtenerCategoriaValidator>();

            #endregion

            #region MARCA

            services.AddScoped<IValidator<RegistrarMarcaRequest>, RegistrarMarcaValidator>();
            services.AddScoped<IValidator<ActualizarMarcaRequest>, ActualizarMarcaValidator>();
            services.AddScoped<IValidator<ActualizarActivoMarcaRequest>, ActualizarActivoMarcaValidator>();
            services.AddScoped<IValidator<ObtenerMarcaRequest>, ObtenerMarcaValidator>();

            #endregion

            #endregion

            #region CLIENTE

            services.AddScoped<IValidator<RegistrarClienteRequest>, RegistrarClienteValidator>();
            services.AddScoped<IValidator<ActualizarClienteRequest>, ActualizarClienteValidator>();
            services.AddScoped<IValidator<ActualizarActivoClienteRequest>, ActualizarActivoClienteValidator>();
            services.AddScoped<IValidator<ObtenerClienteRequest>, ObtenerClienteValidator>();
            services.AddScoped<IValidator<ObtenerClientePorNumDocumentoCorreoRequest>, ObtenerClientePorNumDocumentoCorreoValidator>();

            #endregion

            #region PRODUCTO

            services.AddScoped<IValidator<RegistrarProductoRequest>, RegistrarProductoValidator>();
            services.AddScoped<IValidator<ActualizarProductoRequest>, ActualizarProductoValidator>();
            services.AddScoped<IValidator<ActualizarActivoProductoRequest>, ActualizarActivoProductoValidator>();
            services.AddScoped<IValidator<ObtenerProductoRequest>, ObtenerProductoValidator>();
            services.AddScoped<IValidator<ObtenerProductoPorCodigoRequest>, ObtenerProductoPorCodigoValidator>();
            #endregion

            #region VENTA

            services.AddScoped<IValidator<ObtenerDetalleVentaRequest>, ObtenerDetalleVentaValidator>();
            services.AddScoped<IValidator<ObtenerVentaRequest>, ObtenerVentaValidator>();
            services.AddScoped<IValidator<RegistrarVentaRequest>, RegistrarVentaValidator>();
            services.AddScoped<IValidator<RegistrarDetalleVentaRequest>, RegistrarDetalleVentaValidator>();
            services.AddScoped<IValidator<AnularVentaRequest>, AnularVentaValidator>();
            services.AddScoped<IValidator<ObtenerReporteCategoriaRequest>, ObtenerReporteCategoriaValidator>();
            services.AddScoped<IValidator<ObtenerReporteMarcaRequest>, ObtenerReporteMarcaValidator>();
            services.AddScoped<IValidator<ObtenerReporteProductoRequest>, ObtenerReporteProductoValidator>();
            services.AddScoped<IValidator<ObtenerResumenReporteRequest>, ObtenerResumenReporteValidator>();

            #endregion

            #region PARÁMETRO

            services.AddScoped<IValidator<ActualizarParametroDetalleRequest>, ActualizarParametroDetalleValidator>();

            #endregion

            #endregion

            #region Tools 
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEncriptService, EncriptService>();
            services.AddScoped<IJwTokenGenerator, JwTokenGenerator>();
            #endregion
        }
    }
}
