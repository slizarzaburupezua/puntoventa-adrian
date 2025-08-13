using AutoMapper;
using Microsoft.Extensions.Configuration;
using PERFISOFT.VENTASPLATFORM.APPLICATION.COMMON;
using PERFISOFT.VENTASPLATFORM.APPLICATION.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Interface;
using PERFISOFT.VENTASPLATFORM.DOMAIN.VO.Usuario.Filtro;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.Extensions;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Response;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService;
using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;
using Serilog;
using DictionaryError = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.DictionaryErrors;
using DictionaryInformation = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;
using GS = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Dictionary;

namespace PERFISOFT.VENTASPLATFORM.APPLICATION.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEncriptService _passwordService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToolService _toolService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UsuarioService(IMapper mapper,
                   IUsuarioRepository usuarioRepository,
                   IJwTokenGenerator jwTokenGenerator,
                   IEncriptService passwordService,
                   IUnitOfWork unitOfWork,
                   IEmailService emailService,
                   IConfiguration configuration,
                   IToolService toolService,
                   ICloudinaryService cloudinaryService
                   )
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
            _toolService = toolService;
            _cloudinaryService = cloudinaryService;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<UsuarioDTO> GetPersonalInfoAsync(Guid idUsuarioGuid)
        {
            var idUsuario = await GetIdUsuarioByGuid(idUsuarioGuid);

            return _mapper.Map<UsuarioDTO>(await _usuarioRepository.SelectDetailByIdAsync(idUsuario));
        }

        public async Task<IList<UsuarioDTO>> GetAllByFilterAsync(ObtenerUsuarioRequest request)
        {
            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuario);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Usuario.GetAllUserByFilterAsync.UsuarioNoExiste, request.IdUsuario);
                return new List<UsuarioDTO>();
            }

            return _mapper.Map<List<UsuarioDTO>>(await _usuarioRepository.SelectAllByFilterAsync(_mapper.Map<FiltroConsultaUsuario>(request), idUsuario));
        }

        public async Task<IList<ObtenerColaboradoresActivosDTO>> GetAllActivesAsync(ObtenerColaboradoresRequest request)
        {
            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuario);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Usuario.GetAllUserByFilterAsync.UsuarioNoExiste, request.IdUsuario);
                return new List<ObtenerColaboradoresActivosDTO>();
            }

            return _mapper.Map<List<ObtenerColaboradoresActivosDTO>>(await _usuarioRepository.SelectAllActivesAsync());
        }

        public async Task<ResponseDTO> ExistCorreoAsync(ExistCorreoUsuarioRequest request)
        {
            Log.Information(LogMessages.Usuario.ExistCorreoAsync.Initial, request.Correo);

            if (await _usuarioRepository.ExistCorreoAsync(request.Correo))
            {
                Log.Warning(LogMessages.Usuario.ExistCorreoAsync.UsuarioExiste, request.Correo);
                return CreateResponse(ErrorCodigo.Advertencia, Flags.Existe, string.Format(LogMessages.Usuario.ExistCorreoAsync.UsuarioExiste, request.Correo), string.Empty);
            }

            return CreateResponse(string.Empty, Flags.NoExiste, string.Empty, string.Empty);
        }

        public async Task<ResponseDTO> ExistNumeroDocumentoAsync(ExistNumeroDocumentoUsuarioRequest request)
        {
            Log.Information(LogMessages.Usuario.ExistNumeroDocumentoAsync.Initial, request.NumeroDocumento);

            if (await _usuarioRepository.ExistNumeroDocumentoAsync(request.NumeroDocumento))
            {
                Log.Warning(LogMessages.Usuario.ExistNumeroDocumentoAsync.UsuarioExiste, request.NumeroDocumento);
                return CreateResponse(ErrorCodigo.Advertencia, Flags.Existe, string.Format(LogMessages.Usuario.ExistNumeroDocumentoAsync.UsuarioExiste, request.NumeroDocumento), string.Empty);
            }

            return CreateResponse(string.Empty, Flags.NoExiste, string.Empty, string.Empty);
        }

        public async Task<ResponseDTO> InsertAsync(RegistrarUsuarioRequest request)
        {
            Log.Information(string.Format(DictionaryInformation.LogInicioRegistroUsuario, request.Correo_Electronico));

            if (await _usuarioRepository.ExistCorreoAsync(request.Correo_Electronico.Trim()))
            {
                Log.Warning(LogMessages.Usuario.ExistCorreoAsync.UsuarioExiste, request.Correo_Electronico);
                return CreateResponse(ErrorCodigo.Advertencia, Flags.WarningTransaction, string.Format(LogMessages.Usuario.ExistCorreoAsync.UsuarioExiste, request.Correo_Electronico), string.Empty);
            }

            if (await _usuarioRepository.ExistNumeroDocumentoAsync(request.NumeroDocumento.Trim()))
            {
                Log.Warning(LogMessages.Usuario.ExistNumeroDocumentoAsync.UsuarioExiste, request.NumeroDocumento);
                return CreateResponse(ErrorCodigo.Advertencia, Flags.WarningTransaction, string.Format(LogMessages.Usuario.ExistNumeroDocumentoAsync.UsuarioExiste, request.NumeroDocumento), string.Empty);
            }

            if (!ValidarPoliticaContrasenia(request.Contrasenia))
            {
                Log.Error(LogMessages.AddUsuarioAsync.NoCumplePoliticaContrasesnia, request.Correo_Electronico);
                return CreateErrorResponse(DictionaryError.AutenticacionValidacionContrasenia);
            }

            Log.Information(LogMessages.AddUsuarioAsync.GenerandoSALT, request.Correo_Electronico);

            string saltPassword = _passwordService.GenerateSaltPassword();

            Log.Information(LogMessages.AddUsuarioAsync.GenerandoHASH, request.Correo_Electronico);

            string hashPassword = _passwordService.CreateHashPassword(request.Contrasenia, saltPassword);

            Log.Information(LogMessages.AddUsuarioAsync.InsertUsuario, request.Correo_Electronico);

            var usuarioEntity = await _usuarioRepository.InsertAsync(_mapper.Map<Usuario>(request));

            Log.Information(LogMessages.AddUsuarioAsync.InsertUsuarioSuccess, request.Correo_Electronico);

            Log.Information(LogMessages.AddUsuarioAsync.InsertCredentials, request.Correo_Electronico);

            Guid idUsuarioGuid = Guid.NewGuid();

            await RegistrarUsuarioContrasenaAsync(usuarioEntity.ID, hashPassword, saltPassword, request.DestinationTimeZoneIdRegistro);

            await RegistrarUsuarioIdGuidAsync(usuarioEntity.ID, idUsuarioGuid, request.DestinationTimeZoneIdRegistro);

            await _unitOfWork.SaveAsync();

            Log.Information(LogMessages.AddUsuarioAsync.PreparingEmail, request.Correo_Electronico);

                var path = Path.Combine("templates", "html", "bienvenida.html");

                var text = string.Empty;

                using (var file = new FileStream(path, FileMode.Open))
                {
                    using (var stream = new StreamReader(file))
                        text = await stream.ReadToEndAsync();
                };

                text = text.Replace("{NOMBREUSUARIO}", usuarioEntity.NOMBRES);

                string asunto = _configuration.GetSection("AsuntoRegistroUsuario").Value;

                await _emailService.SendNotificationAsync(request.Correo_Electronico, text, asunto);

            Log.Information(LogMessages.AddUsuarioAsync.CorreoBienvenidaEnviadaCorrectamente, request.Correo_Electronico);

            Log.Information(string.Format(DictionaryInformation.LogRegistroUsuarioExito, idUsuarioGuid));

            return new ResponseDTO()
            {
                Success = Flags.SuccessTransaction,
                Message = DictionaryInformation.SuccessAddUser,
            };
        }

        public async Task<ResponseDTO> EnviarEnlacePagoAsync(EnviarEnlacePagoRequest request)
        {
            Log.Information(LogMessages.EnviarEnlacePagoAsync.PreparingEmail, request.Correo);

            // Reemplazar {NOMBREUSUARIO} en el enlace original
            string enlaceWhatsApp = $"https://wa.me/51972555506?text=Hola%20Steven%20%F0%9F%91%8B%2C%20me%20llamo%20{Uri.EscapeDataString(request.Nombre)}%20y%20estoy%20interesado%20en%20el%20c%C3%B3digo%20fuente%20del%20SISTEMA%20DE%20VENTAS%20desarrollado%20en%20Angular%2018%20%E2%9A%A1%2C%20.NET%209%20%F0%9F%92%BB%20y%20SQL%20%F0%9F%97%84%EF%B8%8F";
            string asunto = _configuration.GetSection("EnlacePago").Value;
            string htmlBody = $@"
        <html>
        <body style='font-family: Arial, sans-serif; color: #333;'>
            <p>Hola <strong>{request.Nombre}</strong>,</p>
            <p>Gracias por tu interés en nuestro sistema de ventas.</p>
            <p>Puedes contactarme directamente vía WhatsApp haciendo clic en el siguiente botón:</p>
            <a href='{enlaceWhatsApp}' style='
                background-color: #25D366;
                color: white;
                padding: 12px 20px;
                text-decoration: none;
                border-radius: 5px;
                font-weight: bold;
                display: inline-block;
                font-size: 16px;
            '>💬 Contactar por WhatsApp</a>

            <br>
            <p>Saludos,<br/>El equipo de Perfisoft</p>
        </body>
        </html>";


            await _emailService.SendNotificationAsync(request.Correo, htmlBody, asunto);


            Log.Information(LogMessages.EnviarEnlacePagoAsync.SuccessEnvioCorreo, request.Correo);

            return new ResponseDTO()
            {
                Success = Flags.SuccessTransaction,
                Message = string.Format(LogMessages.SuccessEnvioCorreoEnlacePago, request.Correo)
            };
        }

        public async Task<ResponseDTO> UpdateAsync(ActualizarUsuarioRequest request)
        {
            Log.Information(LogMessages.Usuario.UpdateAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario == Numeracion.Cero)
            {
                Log.Warning(LogMessages.Usuario.UpdateAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var idUsuarioSeleccionado = await GetIdUsuarioByGuid(request.IdUsuarioSeleccionado);
            if (idUsuarioSeleccionado == Numeracion.Cero)
            {
                Log.Warning(LogMessages.Usuario.UpdateAsync.UsuarioSeleccionadoNoExiste, request.IdUsuarioSeleccionado);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var usuarioSeleccionadoEntity = await _usuarioRepository.SelectByIdAsync(idUsuarioSeleccionado);
            if (usuarioSeleccionadoEntity == null)
            {
                Log.Warning(LogMessages.Usuario.UpdateAsync.UsuarioNoExiste, idUsuarioSeleccionado);
                return CreateErrorEntityResponse(idUsuarioSeleccionado);
            }

            ActualizarDatosUsuario(usuarioSeleccionadoEntity, request);

            if (request.Foto != usuarioSeleccionadoEntity.URLFOTO)
                await GestionarFoto(usuarioSeleccionadoEntity, request.Foto, request.NombreArchivo);

            var updateUsuario = await _usuarioRepository.UpdateAsync(usuarioSeleccionadoEntity);

            Log.Information(LogMessages.Usuario.UpdateAsync.Finish, request.IdUsuarioGuid);
            return CreateSuccessResponse(updateUsuario.ID, updateUsuario.URLFOTO);
        }

        public async Task<ResponseDTO> DeleteAsync(EliminarUsuarioRequest request)
        {
            Log.Information(LogMessages.Usuario.DeleteAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Usuario.DeleteAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var idUsuarioSeleccionado = await GetIdUsuarioByGuid(request.IdUsuarioSeleccionado);

            if (idUsuarioSeleccionado is Numeracion.Cero)
            {
                Log.Warning(LogMessages.Usuario.DeleteAsync.UsuarioSeleccionadoNoExiste, request.IdUsuarioSeleccionado);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var usuarioSeleccionadoEntity = await _usuarioRepository.SelectByIdAsync(idUsuarioSeleccionado);

            if (usuarioSeleccionadoEntity is null)
            {
                Log.Error(LogMessages.Usuario.DeleteAsync.UsuarioActualizarNoExiste, idUsuarioSeleccionado);
                return CreateErrorEntityResponse(idUsuarioSeleccionado);
            }

            usuarioSeleccionadoEntity.ESTADO = Flags.Deshabilitar;
            usuarioSeleccionadoEntity.FECHA_ANULACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
            usuarioSeleccionadoEntity.MOTIVO_ANULACION = request.MotivoAnulacion ?? string.Empty;

            var modifyCategoriaEntity = await _usuarioRepository.UpdateAsync(usuarioSeleccionadoEntity);

            Log.Information(LogMessages.Usuario.DeleteAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(usuarioSeleccionadoEntity.ID, string.Empty);
        }

        public async Task<ResponseDTO> UpdateUsuarioContraseniaByIdAsync(ActualizarContraseniaRequest request)
        {
            Log.Information(LogMessages.UpdateUsuarioContraseniaByIdAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.UpdateUsuarioContraseniaByIdAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return new ResponseDTO();
            }

            if (!ValidarPoliticaContrasenia(request.ContraseniaActual))
            {
                Log.Error(LogMessages.UpdateUsuarioContraseniaByIdAsync.NoCumplePoliticaContrasenia, request.IdUsuarioGuid);
                return CreateWarningResponse(DictionaryError.AutenticacionValidacionContrasenia);
            }

            if (!ValidarPoliticaContrasenia(request.ContraseniaNueva))
            {
                Log.Error(LogMessages.UpdateUsuarioContraseniaByIdAsync.NoCumplePoliticaContrasenia, request.IdUsuarioGuid);
                return CreateWarningResponse(DictionaryError.AutenticacionValidacionContrasenia);
            }

            var usuarioContrasena = await _usuarioRepository.SelectUsuarioContraseniaByIdAsync(idUsuario);

            Log.Information(LogMessages.UpdateUsuarioContraseniaByIdAsync.GenerandoHASHValidacion, request.IdUsuarioGuid);

            string hashContraseniaActual = _passwordService.CreateHashPassword(request.ContraseniaActual, usuarioContrasena.SALT);

            if (usuarioContrasena.CLAVE_HASH != hashContraseniaActual)
            {
                Log.Error(LogMessages.UpdateUsuarioContraseniaByIdAsync.ContraseniaNoEsIgual, request.IdUsuarioGuid);
                return CreateWarningResponse(DictionaryError.ClaveActualIncorrecta);
            }

            Log.Information(LogMessages.UpdateUsuarioContraseniaByIdAsync.GenerandoSALT, request.IdUsuarioGuid);

            string saltPassword = _passwordService.GenerateSaltPassword();

            Log.Information(LogMessages.UpdateUsuarioContraseniaByIdAsync.GenerandoNuevoHASH, request.IdUsuarioGuid);

            string hashPassword = _passwordService.CreateHashPassword(request.ContraseniaNueva, saltPassword);

            usuarioContrasena.CLAVE_HASH = hashPassword;
            usuarioContrasena.SALT = saltPassword;
            usuarioContrasena.ESTADO = Flags.Habilitar;
            usuarioContrasena.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);

            Log.Information(LogMessages.UpdateUsuarioContraseniaByIdAsync.UpdateUsuarioContrasenia, request.IdUsuarioGuid);

            await _usuarioRepository.UpdateUsuarioContraseniaAsync(usuarioContrasena);

            Log.Information(LogMessages.UpdateUsuarioContraseniaByIdAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(GS.SuccessChangePassword);
        }

        public async Task<ResponseDTO> UpdateActivoAsync(ActualizarActivoUsuarioRequest request)
        {
            Log.Information(LogMessages.Usuario.UpdateActivoAsync.Initial, request.IdUsuarioGuid);

            var idUsuario = await GetIdUsuarioByGuid(request.IdUsuarioGuid);

            if (idUsuario is Numeracion.Cero)
            {
                Log.Error(LogMessages.Usuario.UpdateActivoAsync.UsuarioNoExiste, request.IdUsuarioGuid);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            var idUsuarioSeleccionado = await GetIdUsuarioByGuid(request.IdUsuarioSeleccionado);

            if (idUsuarioSeleccionado is Numeracion.Cero)
            {
                Log.Warning(LogMessages.Usuario.UpdateActivoAsync.UsuarioSeleccionadoNoExiste, request.IdUsuarioSeleccionado);
                return CreateErrorUserResponse(request.IdUsuarioGuid);
            }

            await _usuarioRepository.UpdateActivoAsync(idUsuarioSeleccionado, request.Activo);

            Log.Information(LogMessages.Usuario.UpdateActivoAsync.Finish, request.IdUsuarioGuid);

            return CreateSuccessResponse(idUsuarioSeleccionado, string.Empty);
        }

        private void ActualizarDatosUsuario(Usuario usuario, ActualizarUsuarioRequest request)
        {
            usuario.ID_ROL = request.IdRol;
            usuario.CELULAR = request.Celular?.Trim() ?? string.Empty;
            usuario.DIRECCION = request.Direccion?.Trim() ?? string.Empty;
            usuario.FECHA_ACTUALIZACION = DateTime.UtcNow.ConvertDateTimeClient(request.DestinationTimeZoneIdActualizacion);
        }

        #region Private Methods

        private async Task GestionarFoto(Usuario usuario, string nuevaFotoBase64, string nombreArchivoFoto)
        {
            if (string.IsNullOrEmpty(nuevaFotoBase64))
            {
                if (!string.IsNullOrEmpty(usuario.URLFOTO))
                {
                    await _cloudinaryService.DeleteImageAsync(usuario.ID_FOTO);
                    usuario.ID_FOTO = string.Empty;
                    usuario.URLFOTO = string.Empty;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(usuario.URLFOTO) && _toolService.EsBase64(nuevaFotoBase64))
                {
                    var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderUsuarios);
                    usuario.ID_FOTO = fotoResponse.PublicId;
                    usuario.URLFOTO = fotoResponse.SecureURL;
                }
                else
                {
                    if (nuevaFotoBase64 != usuario.URLFOTO && !string.IsNullOrEmpty(usuario.URLFOTO))
                    {
                        await _cloudinaryService.DeleteImageAsync(usuario.ID_FOTO);

                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderUsuarios);
                        usuario.ID_FOTO = fotoResponse.PublicId;
                        usuario.URLFOTO = fotoResponse.SecureURL;
                    }
                    else
                    {
                        var fotoResponse = await _cloudinaryService.UploadImageAsync(nombreArchivoFoto, nuevaFotoBase64, Parametros.FolderUsuarios);
                        usuario.ID_FOTO = fotoResponse.PublicId;
                        usuario.URLFOTO = fotoResponse.SecureURL;
                    }

                }
            }
        }

        private bool ValidarPoliticaContrasenia(string pass)
        {
            if (!pass.Any(char.IsUpper) || !pass.Any(char.IsLower) || pass.Length < Numeracion.Doce || !pass.Any(char.IsDigit))
                return false;

            return true;
        }

        private ResponseDTO CreateWarningResponse(string message)
        {
            return new ResponseDTO
            {
                Code = ErrorCodigo.Advertencia,
                Success = Flags.ErrorTransaction,
                TitleMessage = DictionaryError.ErrorTitleTransaction,
                Message = message
            };
        }

        private ResponseDTO CreateSuccessResponse(string message)
        {
            return new ResponseDTO
            {
                Success = Flags.SuccessTransaction,
                TitleMessage = GS.SuccessTitleTransaction,
                Message = message
            };
        }

        private ResponseDTO CreateErrorUserResponse(Guid idUsuario)
        {
            return new ResponseDTO
            {
                IdUsuario = idUsuario,
                Code = ErrorCodigo.Error,
                Success = Flags.ErrorTransaction,
                TitleMessage = DictionaryError.ErrorTitleTransaction,
                Message = DictionaryError.ErrorTransaction
            };
        }

        private ResponseDTO CreateErrorEntityResponse(int idEntity)
        {
            return new ResponseDTO
            {
                Id = idEntity,
                Code = ErrorCodigo.Error,
                Success = Flags.ErrorTransaction,
                TitleMessage = DictionaryError.ErrorTitleTransaction,
                Message = DictionaryError.ErrorTransaction
            };
        }

        private ResponseDTO CreateSuccessResponse(int id, string value)
        {
            return new ResponseDTO
            {
                Id = id,
                Success = Flags.SuccessTransaction,
                TitleMessage = DictionaryInformation.SuccessTitleTransaction,
                Message = DictionaryInformation.SuccessTransaction,
                Value = value
            };
        }

        private async Task<int> GetIdUsuarioByGuid(Guid idUsuario)
        {
            return await _usuarioRepository.GetIdUsuarioByGuid(idUsuario);
        }

        private async Task RegistrarUsuarioIdGuidAsync(int idUsuario, Guid idUsuarioGuid, string destinationTimeZoneIdRegistro)
        {
            var oUsuarioIdHashEntity = new UsuarioId();

            oUsuarioIdHashEntity.ID_USUARIO = idUsuario;
            oUsuarioIdHashEntity.ID_USUARIO_GUID = idUsuarioGuid;
            oUsuarioIdHashEntity.ESTADO = Flags.Habilitar;
            oUsuarioIdHashEntity.FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(destinationTimeZoneIdRegistro);

            await _usuarioRepository.AddUsuarioIdGuidAsync(oUsuarioIdHashEntity);
        }

        private async Task RegistrarUsuarioContrasenaAsync(int idUsuario, string hashPassword, string saltPassword, string destinationTimeZoneIdRegistro)
        {
            var usuarioContrasena = new UsuarioContrasena();
            usuarioContrasena.ID_USUARIO = idUsuario;
            usuarioContrasena.CLAVE_HASH = hashPassword;
            usuarioContrasena.SALT = saltPassword;
            usuarioContrasena.ESTADO = Flags.Habilitar;
            usuarioContrasena.FECHA_REGISTRO = DateTime.UtcNow.ConvertDateTimeClient(destinationTimeZoneIdRegistro);

            await _usuarioRepository.AddUsuarioClaveAsync(usuarioContrasena);
        }

        private ResponseDTO CreateErrorResponse(string message)
        {
            return new ResponseDTO
            {
                Code = ErrorCodigo.Error,
                Success = Flags.ErrorTransaction,
                TitleMessage = DictionaryError.ErrorTitleTransaction,
                Message = message
            };
        }

        private ResponseDTO CreateResponse(string code, bool Success, string message, string value)
        {
            return new ResponseDTO
            {
                Code = code,
                Success = Success,
                Message = message,
                Value = value
            };
        }

        #endregion

    }
}
