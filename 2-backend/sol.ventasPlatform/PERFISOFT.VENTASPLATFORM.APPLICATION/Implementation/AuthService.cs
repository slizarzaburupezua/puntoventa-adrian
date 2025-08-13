using AutoMapper;
using Microsoft.Extensions.Configuration;
using PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Response;
using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;
using Serilog;
using DE = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using DictionaryError = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using DictionaryInformation = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwTokenGenerator _jwTokenGenerator;
        private readonly IEncriptService _passwordService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IParametrosGeneralesRepository _parametrosGeneralesRepository;
        private readonly IToolService _toolService;

        public AuthService(IMapper mapper,
                           IUsuarioRepository usuarioRepository,
                           IJwTokenGenerator jwTokenGenerator,
                           IEncriptService passwordService,
                           IUnitOfWork unitOfWork,
                           IEmailService emailService,
                           IParametrosGeneralesRepository parametrosGeneralesRepository,
                           IConfiguration configuration,
                           IToolService toolService
                           )
        {
            _usuarioRepository = usuarioRepository;
            _jwTokenGenerator = jwTokenGenerator;
            _parametrosGeneralesRepository = parametrosGeneralesRepository;
            _passwordService = passwordService;
            _emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
            _toolService = toolService;
        }

        public async Task<IniciaSesionDTO> IniciaSesionAsync(IniciaSesionRequest request)
        {
            Log.Information(LogMessages.IniciaSesionAsync.Initial, request.Correo);

            if (!await _usuarioRepository.ExistCorreoAsync(request.Correo))
            {
                Log.Warning(LogMessages.IniciaSesionAsync.CorreoNoExiste, request.Correo);
                return new IniciaSesionDTO()
                {
                    Response = _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.NoTieneAcceso, DE.UsuarioNoRegistrado, string.Empty)
                };
            }

            var usuario = await _usuarioRepository.GetLoginAsync(request.Correo);

            if (!usuario.ACTIVO)
            {
                Log.Warning(LogMessages.IniciaSesionAsync.UsuarioNoActivo, request.Correo);
                return new IniciaSesionDTO()
                {
                    Response = _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.NoTieneAcceso, DictionaryInformation.WarningUsuarioNoActivo, string.Empty)
                };
            }

            var salt = await _usuarioRepository.GetSaltAsync(usuario.ID);

            var claveHash = _passwordService.CreateHashPassword(request.Clave, salt);

            if (!await _usuarioRepository.ValidaClaveHashAsync(claveHash, usuario.ID))
            {
                Log.Error(LogMessages.IniciaSesionAsync.UsuarioNoExiste, request.Correo);
                return new IniciaSesionDTO()
                {
                    Response = _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.NoTieneAcceso, DE.ClaveIncorrecta, string.Empty)
                };
            }

            var idUsuarioGuid = await _usuarioRepository.GetIdGuidUserById(usuario.ID); 

            var idMonedaNegocio = await _parametrosGeneralesRepository.GetIdMonedaNegocioAsync();

            var monedaNegocio = _mapper.Map<MonedaDTO>(await _parametrosGeneralesRepository.GetMonedaByIdAsync(idMonedaNegocio));

            var tokenSesion = _jwTokenGenerator.GenerateTokenSignIn(idUsuarioGuid, usuario.NOMBRES, usuario.APELLIDOS, usuario.CORREO_ELECTRONICO, usuario.ROL.ID, usuario.ROL.NOMBRE, idMonedaNegocio, usuario.ID_FOTO, usuario.URLFOTO);

            var rolUsuario = _mapper.Map<List<MenuRolDTO>>(await _parametrosGeneralesRepository.GetAllMenuRolAsync(usuario.ROL.ID));

            Log.Information(LogMessages.IniciaSesionAsync.SuccessIniciaSesion, request.Correo);

            return new IniciaSesionDTO()
            {
                Response = _toolService.CreateResponse(string.Empty, Flags.TieneAcceso, string.Empty, tokenSesion),
                MenuRol = rolUsuario,
                Moneda = monedaNegocio
            };

        }

        public async Task<ResponseDTO> NotifyOlvideContraseniaAsync(NotifyOlvideContraseniaRequest request)
        {
            Log.Information(LogMessages.NotifyOlvideContraseniaAsync.Initial, request.Correo);

            if (!await _usuarioRepository.ExistCorreoAsync(request.Correo))
            {
                Log.Error(LogMessages.NotifyOlvideContraseniaAsync.CorreoNoExiste, request.Correo);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.NoExiste, DictionaryInformation.WarningNoExisteUsuario, string.Empty);
            }

            var usuarioRegistroToken = await _usuarioRepository.SelectUsuarioRestableceContraseniaByCorreoAsync(request.Correo);

            if (usuarioRegistroToken is not null)
            {
                var fechaActual = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZone);

                var diferenciaFechaCodigo = fechaActual - usuarioRegistroToken.FECHA_REGISTRO;

                if (diferenciaFechaCodigo.TotalMinutes > Numeracion.Diez)
                {
                    usuarioRegistroToken.ESTADO = Flags.TokenCaducado;
                    usuarioRegistroToken.MOTIVO_ANULACION = DictionaryInformation.TokenExpirado;
                    usuarioRegistroToken.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZone);
                    usuarioRegistroToken.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZone);

                    await _usuarioRepository.UpdateUsuarioTokenRestableceContraseniaAsync(usuarioRegistroToken);
                }
                else
                {
                    usuarioRegistroToken.ESTADO = Flags.TokenActualizado;
                    usuarioRegistroToken.MOTIVO_ANULACION = DictionaryInformation.TokenActualizado;
                    usuarioRegistroToken.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZone);
                    usuarioRegistroToken.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZone);

                    await _usuarioRepository.UpdateUsuarioTokenRestableceContraseniaAsync(usuarioRegistroToken);
                }
            }

            string tokenUrl = await GenerateTokenOlvideContraseniaAsync(request);

            string nombreUsuario = await _usuarioRepository.SelectNombreUsuarioByCorreoAsync(request.Correo);

            await EnviarCorreoRestablecerContraseniaAsync(request.Correo, nombreUsuario, tokenUrl);

            string correoConvertido = _toolService.ConvertCorreo(request.Correo);

            Log.Information(LogMessages.NotifyOlvideContraseniaAsync.CorreoEnviado, request.Correo);

            return _toolService.CreateResponse(string.Empty, Flags.TokenGenerado,
                                  string.Format(DictionaryInformation.SuccessTokenOlvideContraseniaGeneradoCorrectamente,
                                  correoConvertido), string.Empty);
        }

        public async Task<ResponseDTO> RestablecerContraseniaAsync(RestablecerContraseniaRequest request)
        {
            Log.Information(LogMessages.RestablecerContraseniaAsync.Initial, request.Token);

            if (!ValidarPoliticaContrasenia(request.Contrasenia))
            {
                Log.Error(LogMessages.RestablecerContraseniaAsync.Initial, request.Token);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.NoExiste, DictionaryError.AutenticacionValidacionContrasenia, string.Empty);
            }

            string correoDesCifrado = _passwordService.OpenSSLDecrypt(request.Token!.Replace("|#|", "/"));

            var usuarioRegistroToken = await _usuarioRepository.SelectUsuarioRestableceContraseniaByCorreoAsync(correoDesCifrado);

            string urlSplitRegistroToken = usuarioRegistroToken.TOKEN.Split("/reset-password/")[1];

            if (request.Token != urlSplitRegistroToken)
            {
                Log.Error(LogMessages.RestablecerContraseniaAsync.EnlaceNoValido, request.Token);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.ErrorTransaction,
                                  DictionaryInformation.WarningTokenOlvideContraseniaNoValido, string.Empty);

            }

            if (usuarioRegistroToken is null || usuarioRegistroToken.VERIFICADO || !usuarioRegistroToken.ESTADO)
            {
                Log.Error(LogMessages.RestablecerContraseniaAsync.EnlaceNoValido, request.Token);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.ErrorTransaction,
                                  DictionaryInformation.WarningTokenOlvideContraseniaNoValido, string.Empty);
            }

            Log.Information(LogMessages.RestablecerContraseniaAsync.BuscandoCorreoCifrado, correoDesCifrado);

            var idUsuario = await _usuarioRepository.SelectIdByCorreoAsync(correoDesCifrado);

            Log.Information(LogMessages.RestablecerContraseniaAsync.UsuarioIdentificador, idUsuario, correoDesCifrado);

            Log.Information(LogMessages.RestablecerContraseniaAsync.UsuarioIdentificador, idUsuario, correoDesCifrado);

            var usuarioContrasena = await _usuarioRepository.SelectUsuarioContraseniaByIdAsync(idUsuario);

            Log.Information(LogMessages.RestablecerContraseniaAsync.GenerandoSALT, idUsuario, correoDesCifrado);

            string saltPassword = _passwordService.GenerateSaltPassword();

            Log.Information(LogMessages.RestablecerContraseniaAsync.GenerandoHASH, idUsuario, correoDesCifrado);

            string hashPassword = _passwordService.CreateHashPassword(request.Contrasenia, saltPassword);

            usuarioContrasena.CLAVE_HASH = hashPassword;
            usuarioContrasena.SALT = saltPassword;
            usuarioContrasena.ESTADO = Flags.Habilitar;
            usuarioContrasena.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);

            Log.Information(LogMessages.RestablecerContraseniaAsync.UpdateUsuarioContrasenia, correoDesCifrado);

            await _usuarioRepository.UpdateUsuarioContraseniaAsync(usuarioContrasena);

            usuarioRegistroToken.VERIFICADO = Flags.TokenVerificado;
            usuarioRegistroToken.ESTADO = Flags.DeshabilitarToken;
            usuarioRegistroToken.MOTIVO_ANULACION = DictionaryInformation.TokenValidado;
            usuarioRegistroToken.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            usuarioRegistroToken.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);

            Log.Information(LogMessages.RestablecerContraseniaAsync.UsuarioRestableceContrasenia, correoDesCifrado);

            await _usuarioRepository.UpdateUsuarioTokenRestableceContraseniaAsync(usuarioRegistroToken);

            Log.Information(LogMessages.RestablecerContraseniaAsync.PreparingEmail, correoDesCifrado);

            var path = Path.Combine("templates", "html", "contraseniacambiada.html");

            var text = string.Empty;

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var stream = new StreamReader(file))
                    text = await stream.ReadToEndAsync();
            };

            string nombreUsuario = await _usuarioRepository.SelectNombreUsuarioByCorreoAsync(correoDesCifrado);

            text = text.Replace("{NOMBREUSUARIO}", nombreUsuario);

            string asunto = _configuration.GetSection("AsuntoContraseniaCambiada").Value;

            await _emailService.SendNotificationAsync(correoDesCifrado, text, asunto);

            Log.Information(LogMessages.RestablecerContraseniaAsync.RestablecimientoCorrecto, correoDesCifrado);

            return _toolService.CreateResponse(string.Empty, Flags.SuccessTransaction, DictionaryInformation.SuccessChangePassword, string.Empty);
        }

        public async Task<ResponseDTO> VerifyTokenRestablecerContraseniaAsync(VerifyTokenRestablecerContraseniaRequest request)
        {
            Log.Information(LogMessages.VerifyTokenRestablecerContraseniaAsync.Initial, request.Token);

            string correoDesCifrado = _passwordService.OpenSSLDecrypt(request.Token!.Replace("|#|", "/"));

            Log.Information(LogMessages.VerifyTokenRestablecerContraseniaAsync.CorreoDescrifrado, correoDesCifrado);

            var usuarioRegistroToken = await _usuarioRepository.SelectUsuarioRestableceContraseniaByCorreoAsync(correoDesCifrado);

            if (usuarioRegistroToken is null)
            {
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.ErrorTransaction,
                      DictionaryInformation.WarningTokenOlvideContraseniaNoValido, string.Empty);
            }

            string urlSplitRegistroToken = usuarioRegistroToken.TOKEN.Split("/reset-password/")[1];

            if (request.Token != urlSplitRegistroToken)
            {
                Log.Error(LogMessages.VerifyTokenRestablecerContraseniaAsync.EnlaceNoValido, correoDesCifrado, urlSplitRegistroToken);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.ErrorTransaction,
                                      DictionaryInformation.WarningTokenOlvideContraseniaNoValido, string.Empty);
            }

            if (usuarioRegistroToken is null || usuarioRegistroToken.VERIFICADO || !usuarioRegistroToken.ESTADO)
            {
                Log.Error(LogMessages.VerifyTokenRestablecerContraseniaAsync.EnlaceNoValido, correoDesCifrado, usuarioRegistroToken);
                return _toolService.CreateResponse(ErrorCodigo.Advertencia, Flags.ErrorTransaction,
                                       DictionaryInformation.WarningTokenOlvideContraseniaNoValido, string.Empty);

            }

            Log.Information(LogMessages.VerifyTokenRestablecerContraseniaAsync.SuccessVerificacion, correoDesCifrado);

            return _toolService.CreateResponse(string.Empty, Flags.SuccessTransaction, string.Empty, correoDesCifrado);
        }

        #region Métodos Privados

        private async Task<string> GenerateTokenOlvideContraseniaAsync(NotifyOlvideContraseniaRequest request)
        {
            string correoCifrado = _passwordService.OpenSSLEncrypt(request.Correo);
            var tokenUrl = String.Concat(_configuration.GetSection("ConfigApplication:urlApplication").Value, Strings.LinkRestablecerContrasenia + correoCifrado.Replace("/", "|#|"));

            UsuarioRestableceContrasenia usuarioToken = new UsuarioRestableceContrasenia()
            {
                CORREO_ELECTRONICO = request.Correo,
                TOKEN = tokenUrl,
                ESTADO = Flags.Habilitado,
                VERIFICADO = Flags.OTPNoVerificado,
                FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZone),
            };

            await _usuarioRepository.InsertUsuarioTokenRestableceContraseniaAsync(usuarioToken);

            return tokenUrl;
        }

        private async Task EnviarCorreoRestablecerContraseniaAsync(string correo, string usuario, string enlacetoken)
        {
            var path = Path.Combine("templates", "html", "restablecercontrasenia.html");

            var text = string.Empty;

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var stream = new StreamReader(file))
                    text = await stream.ReadToEndAsync();
            };

            text = text.Replace("{URLTOKEN}", enlacetoken);
            text = text.Replace("{NOMBREUSUARIO}", usuario);

            string asunto = _configuration.GetSection("AsuntoRestablecerContrasenia").Value;

            await _emailService.SendNotificationAsync(correo, text, asunto);
        }

        private bool ValidarPoliticaContrasenia(string pass)
        {
            if (!pass.Any(char.IsUpper) || !pass.Any(char.IsLower) || pass.Length < Numeracion.Doce || !pass.Any(char.IsDigit))
                return Flags.False;

            return Flags.True;
        }

        #endregion
    }
}
