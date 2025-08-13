namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure
{
    public struct Numeracion
    {
        public const int UsuarioNoExistente = 0;
        public const int Cero = 0;
        public const int Uno = 1;
        public const int Dos = 2;
        public const int Tres = 3;
        public const int Cuatro = 4;
        public const int Cinco = 5;
        public const int Seis = 6;
        public const int Siete = 7;
        public const int Ocho = 8;
        public const int Nueve = 9;
        public const int Diez = 10;
        public const int Once = 11;
        public const int Doce = 12;
        public const int Quince = 15;
        public const int Dieciseis = 16;
        public const int Diecisiete = 17;
        public const int Dieciocho = 18;
        public const int Veinte = 20;
        public const int Treinta = 30;
        public const int Veintitres = 23;
        public const int Veinticuatro = 24;
        public const int Cincuenta = 50;
        public const int Cien = 100;
        public const int Doscientos = 200;
        public const int Quinientos = 500;
        public const int Mil = 1000;
        public const int CientoCuarenta = 140;
        public const int Cuarenta = 40;
        public const int Ochenta = 80;
        public const int BaseDiasGratuitos = 15;
        public const int MaximaCantidadRegistroDia = 5;
    }

    public struct Flags
    {
        public const bool NuevoUsuario = true;
        public const bool NoNuevoUsuario = false;
        public const bool Existe = true;
        public const bool NoExiste = false;
        public const bool Habilitar = true;
        public const bool Deshabilitar = false;
        public const bool SuccessTransaction = true;
        public const bool WarningTransaction = false;
        public const bool ErrorTransaction = false;
        public const bool Habilitado = true;
        public const bool RecibirNotificaciones = true;
        public const bool Renovacion = true;
        public const bool RenovacionCancelada = false;
        public const bool False = false;
        public const bool True = true;
        public const bool RecibirBoletines = true;
        public const bool Activado = true;
        public const bool Desactivado = false;
        public const bool Desactivar = false;
        public const bool Activar = true;
        public const bool NoRequerido = false;
        public const bool OTPCaducado = false;
        public const bool OTPNoExiste = false;
        public const bool OTPNoVerificado = false;
        public const bool OTPVerificado = true;
        public const bool TokenGenerado = true;
        public const bool TokenValido = true;
        public const bool TokenVerificado = true;
        public const bool DeshabilitarToken = false;
        public const bool TokenNoVerificado = false;
        public const bool TokenInvalido = false;
        public const bool TokenCaducado = false;
        public const bool TokenActualizado = false;
        public const bool TieneAcceso = true;
        public const bool NoTieneAcceso = false;
    }

    public struct Strings
    {
        public const string ValidNames = @"^[a-zA-ZáéíóúüñÁÉÍÓÚÜÑ\s'-]*$";
        /// <summary>
        /// la expresión regular ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$ asegura que la contraseña cumpla con las siguientes condiciones:
        /// (?=.*[a-z]): Debe contener al menos una letra minúscula.
        /// (?=.*[A-Z]): Debe contener al menos una letra mayúscula.
        /// (?=.*\d): Debe contener al menos un dígito.
        /// (?=.*[^\da-zA-Z]): Debe contener al menos un carácter que no sea una letra o un número.
        /// </summary>
        public const string SecurePassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$";
        public const string OnlyNumbersFormat = @"^[0-9]*$";
        public const string LinkRestablecerContrasenia = "/reset-password/";
        public const string Todos = "Todos";
    }

    public struct ErrorCodigo
    {
        public const string Advertencia = "1";
        public const string Error = "0";
    }

    public struct TituloResponse
    {
        public const string Advertencia = "Advertencia";
        public const string Error = "Error";
    }

    public struct ResponseMessages
    {
        public const string Error = "Se ha producido un error inesperado. Por favor, contacta a soporte técnico para obtener ayuda";

        public struct Venta
        {
            public const string SuccessVenta = "La Venta Nro. {0} ha sido generada correctamente";
            public const string SuccessAnulacionVenta = "La Venta Nro. {0} ha sido anulada correctamente";
        }

        public struct Inventario
        {
            public struct Categoria
            {
                public const string ExistProductosCategoria = "La categoría no puede eliminarse porque existen productos asociadas";

            }

            public struct Marca
            {
                public const string ExistProductosMarca = "La marca no puede eliminarse porque existen productos asociadas";

            }

            public struct Producto
            {
                public struct ExistCodigoAsync
                {
                    public const string CodigoExiste = "El Código del Producto {0} ya se encuentra registrado";
                }

                public struct ExistNombreProductoAsync
                {
                    public const string NombreExiste = "El Nombre del Producto {0} ya se encuentra registrado";
                }

            }
        }
         
        public struct Negocio
        {
            public const string SuccessInformacionCambiada = "Cambios guardados, vuelva a iniciar sesión para que los cambios se apliquen.";
        }
    }

    public struct Parametros
    {
        public const string SerieVenta = "001";
        public const string FolderUsuarios = "Usuarios";
        public const string FolderNegocio = "Negocio";
        public const string FolderProductos = "Productos";
        public const string FolderParametros = "Parametros";
        public const string FolderBoletasFactura = "BoletasFacturas";
        public const string FolderPreviewBoletasFacturas = "PreviewBoletasFacturas";

        public struct MetodoPago
        {
            public const int Efectivo = 1;
        }

        public struct TipoCampo
        {
            public const string URL = "URL";
            public const string IMAGEN = "IMAGEN";
        }

        public struct ParaKey
        {
            public const string LOGO_STM = "LOGO_STM";
        }

        public struct SubParaKey
        {
            public const string LOGO_PCP = "LOGO_PCP";
            public const string LOGO_HME = "LOGO_HME";
        }

        public struct FormatoImpresion
        {
            public const string TICKETERA = "TKT";
            public const string PDF = "PDF";
        }

    }

    #region Log

    public struct LogMessages
    {
        public struct UpdateUsuarioContraseniaByIdAsync
        {
            public const string Initial = "UpdateUsuarioContraseniaByIdAsync(): Iniciando método. Usuario {IdUsuarioGuid} ";
            public const string UsuarioNoExiste = "UpdateUsuarioContraseniaByIdAsync(): El usuario {IdUsuarioGuid} no se encuentra en la base de datos de BudBle";
            public const string Finish = "UpdateUsuarioContraseniaByIdAsync(): El usuario {IdUsuarioGuid} actualizó su información correctamente";
            public const string Data = "UpdateUsuarioContraseniaByIdAsync():  El usuario {IdUsuarioGuid} Intenta actualizar con los siguientes datos. Data {request}";
            public const string NoCumplePoliticaContrasenia = "UpdateUsuarioContraseniaByIdAsync(): La contraseña ingresada no cumple con las politicas. Usuario {IdUsuarioGuid}";
            public const string ContraseniaNoEsIgual = "UpdateUsuarioContraseniaByIdAsync(): La contraseña ingresada no es igual. Usuario {IdUsuarioGuid}";
            public const string GenerandoSALT = "UpdateUsuarioContraseniaByIdAsync(): Generando SALT para el usuario {IdUsuarioGuid}";
            public const string GenerandoHASHValidacion = "UpdateUsuarioContraseniaByIdAsync(): Generando HASH para la validación de la contraseña actual. Usuario {IdUsuarioGuid}";
            public const string GenerandoNuevoHASH = "UpdateUsuarioContraseniaByIdAsync(): Generando Nuevo HASH para la nueva contraseña. Usuario {IdUsuarioGuid}";
            public const string UpdateUsuarioContrasenia = "UpdateUsuarioContraseniaByIdAsync(): Actualizando datos a la tabla UsuarioContrasena para el usuario: {IdUsuarioGuid}";
        }

        public struct GenerateOTPEmailAsync
        {
            public const string Initial = "GenerateOTPEmailAsync(): Iniciando método. Correo {0}";
            public const string Data = "GenerateOTPEmailAsync(): Insertando datos en la tabla UsuarioRegistroOTP. Data {0}.";
            public const string UsuarioRegistroOTPSuccess = "GenerateOTPEmailAsync(): Datos insertados en la tabla UsuarioRegistroOTP correctamente. Data {0}.";
            public const string UsuarioExiste = "GenerateOTPEmailAsync(): El correo {0} ya se encuentra registrado en la base de datos ";
            public const string OTPGeneradoCorrectamente = "ExistCorreoAsync(): Código OTP Generado correctamente y enviado al correo. {0}";
            public const string OTPValidadoCorrectamente = "ExistCorreoAsync(): Código OTP Ya se encuentra validado correctamente. {0}";
            public const string ELiminandoOTP = "ExistCorreoAsync(): Código OTP Ya se encuentra registrado, eliminando existente. {0}";
            public const string PreparingEmail = "ExistCorreoAsync(): Preparando envio de correo a. {0}";
        }

        public struct VerifyOTPEmailAsync
        {
            public const string Initial = "VerifyOTPEmailAsync(): Iniciando método. Correo {0}";
            public const string Data = "VerifyOTPEmailAsync(): Insertando datos en la tabla UsuarioRegistroOTP. Data {0}.";
            public const string UsuarioRegistroOTPSuccess = "VerifyOTPEmailAsync(): Datos insertados en la tabla UsuarioRegistroOTP correctamente. Data {0}.";
            public const string UsuarioExiste = "VerifyOTPEmailAsync(): El correo {0} ya se encuentra registrado en la base de datos ";
            public const string OTPGeneradoCorrectamente = "VerifyOTPEmailAsync(): Código OTP Generado correctamente y enviado al correo. {0}";
            public const string OTPValidadoCorrectamente = "VerifyOTPEmailAsync(): Código OTP Ya se encuentra validado correctamente. {0}";
            public const string ELiminandoOTP = "VerifyOTPEmailAsync(): Código OTP Ya se encuentra registrado, eliminando existente. {0}";
            public const string PreparingEmail = "VerifyOTPEmailAsync(): Preparando envio de correo a. {0}";
            public const string OTPNoExiste = "VerifyOTPEmailAsync(): Código OTP No se encuentra en el sistema. El código OTP {0} del correo {0} no se encuentra registrado";
            public const string OTPCaducado = "VerifyOTPEmailAsync(): Código OTP caducado. El código OTP {0} del correo {1} ya caducó";
            public const string OTPVerificacionCorrecta = "VerifyOTPEmailAsync(): Código OTP verificado correctamente, puede continuar con el proceso de registro El código OTP {0} del correo {1} ya se validó correctamente";

        }

        public struct AddUsuarioAsync
        {
            public const string ExisteUsuario = "AddUsuarioAsync(): El correo {0} ya se encuentra registrado en la base de datos ";
            public const string NoCumplePoliticaContrasesnia = "AddUsuarioAsync(): La contraseña ingresada no cumple con las politicas. {0}";
            public const string InvalidOTP = "AddUsuarioAsync(): No se ha encontrado la verificación del OTP para el correo electronico {0}";
            public const string GenerandoSALT = "AddUsuarioAsync(): Generando SALT para el correo electronico {0}";
            public const string GenerandoHASH = "AddUsuarioAsync(): Generando HASH para el correo electronico {0}";
            public const string InsertUsuario = "AddUsuarioAsync(): Insertando datos a la tabla Usuario para el Correo: {0}";
            public const string InsertUsuarioSuccess = "AddUsuarioAsync(): Se insertaron los datos correctamente en la tabla Usuario para el Correo: {0}";
            public const string InsertCredentials = "AddUsuarioAsync(): Insertando datos en las tablas correspondientes a las credenciales para el correo: {0}";
            public const string PreparingEmail = "AddUsuarioAsync(): Preparando envio de correo a. {0}";
            public const string CorreoBienvenidaEnviadaCorrectamente = "AddUsuarioAsync(): Correo de bienvenida enviada correctamente al correo {0}";
            public const string SuccessCreacionData = "AddUsuarioAsync(): Se crearon los datos por defecto para el correo {0} con código {1}";

        }

        public struct IniciaSesionAsync
        {
            public const string Initial = "IniciaSesionAsync(): Iniciando método. Correo a iniciar sesión {0}";
            public const string CorreoNoExiste = "IniciaSesionAsync(): El Correo {0} no se encuentra en la base de datos ";
            public const string UsuarioNoActivo = "IniciaSesionAsync(): El Usuario {0} no se encuentra activo";
            public const string UsuarioNoExiste = "IniciaSesionAsync(): El Usuario ha ingresado una contrasña que no corresponde al correo. Correo: {0}";
            public const string SuccessIniciaSesion = "IniciaSesionAsync(): El Usuario ha iniciado sesión correctamente. Correo: {0}";
            public const string ValidacionTipoPlan = "IniciaSesionAsync(): Validando tipo plan del usuario. Correo: {0}";
            public const string UsuarioNoCuentaConPlanSuscripcion = "IniciaSesionAsync(): Usuario no cuenta con una suscripción activa. Usuario: {0}";
        }

        public struct NotifyOlvideContraseniaAsync
        {
            public const string Initial = "NotifyOlvideContraseniaAsync(): Iniciando método. Correo {0}";
            public const string CorreoNoExiste = "NotifyOlvideContraseniaAsync(): El Correo {0} no se encuentra en la base de datos ";
            public const string CorreoEnviado = "NotifyOlvideContraseniaAsync(): Se enviaron las instrucciones al correo {0}";
        }

        public struct RestablecerContraseniaAsync
        {
            public const string Initial = "RestablecerContraseniaAsync(): Iniciando método. Token {0}";
            public const string NoCumplePoliticaContrasenia = "RestablecerContraseniaAsync(): La contraseña ingresada no cumple con las politicas.";
            public const string EnlaceNoValido = "RestablecerContraseniaAsync(): El enlace para reestablecer la contraseña no es correcta. Token {0}";
            public const string BuscandoCorreoCifrado = "RestablecerContraseniaAsync(): Buscando Identificador del usuario con correo descifrado. Correo {0}";
            public const string UsuarioIdentificador = "RestablecerContraseniaAsync(): Identificador del usuario {0} para el correo Correo {1}.";
            public const string GenerandoSALT = "RestablecerContraseniaAsync(): Generando SALT para el correo electronico {0}";
            public const string GenerandoHASH = "RestablecerContraseniaAsync(): Generando HASH para el correo electronico {0}";
            public const string UpdateUsuarioContrasenia = "RestablecerContraseniaAsync(): Actualizando datos a la tabla UsuarioContrasena para el Correo: {0}";
            public const string UsuarioRestableceContrasenia = "RestablecerContraseniaAsync(): Actualizando datos a la tabla UsuarioRestableceContrasenia para el Correo: {0}";
            public const string PreparingEmail = "RestablecerContraseniaAsync(): Preparando envio de correo a. {0}";
            public const string RestablecimientoCorrecto = "RestablecerContraseniaAsync(): La contraseña se ha restablecido correctamente para el correo {0}";
        }

        public struct VerifyTokenRestablecerContraseniaAsync
        {
            public const string Initial = "VerifyTokenRestablecerContraseniaAsync(): Iniciando método. Token {0}";
            public const string CorreoDescrifrado = "VerifyTokenRestablecerContraseniaAsync(): El correo descrifado es {0}";
            public const string EnlaceNoValido = "VerifyTokenRestablecerContraseniaAsync(): El enlace del correo {correo} para reestablecer la contraseña no es correcta. Token {0}";
            public const string SuccessVerificacion = "VerifyTokenRestablecerContraseniaAsync(): Se verificó el Token correctamente para el correo {0}";

        }

        public struct GetPersonalInfoAsync
        {
            public const string Initial = "GetPersonalInfoAsync(): Iniciando método. Usuario {0} ";
            public const string UsuarioNoExiste = "GetPersonalInfoAsync(): El usuario {0} no se encuentra en la base de datos ";
            public const string Finish = "GetPersonalInfoAsync(): El usuario {0} solicitó su información correctamente";
        }

        public struct GetAllMedidaAsync
        {
            public const string Initial = "GetAllMedidaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "GetAllMedidaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string Finish = "GetAllMedidaAsync(): El usuario {0} solicitó las medidas correctamente";
        }

        public struct GetAllCategoriaByFilterAsync
        {
            public const string Initial = "GetAllCategoriaByFilterAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "GetAllCategoriaByFilterAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string Finish = "GetAllCategoriaByFilterAsync(): El usuario {0} solicitó las categorias correctamente";
        }

        public struct EnviarEnlacePagoAsync
        {
            public const string PreparingEmail = "EnviarEnlacePagoAsync(): Preparando envio de correo a. {0}";
            public const string SuccessEnvioCorreo = "EnviarEnlacePagoAsync(): Se envió el correo a {0} correctamente";

        }

        public const string SuccessEnvioCorreoEnlacePago = "Correo enviado a {0}. Revise su bandeja";

        public struct Producto
        {
            public struct GetByCodeAsync
            {
                public const string Initial = "Producto - GetByCodeAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Producto - GetByCodeAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Producto - GetByCodeAsync(): El usuario {0} consultó el producto correctamente";
            }

            public struct GetAllByFilterAsync
            {
                public const string Initial = "Producto - GetAllByFilterAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Producto - GetAllByFilterAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Producto - GetAllByFilterAsync(): El usuario {0} solicitó los productos correctamente";
            }

            public struct InsertAsync
            {
                public const string Initial = "Producto - InsertAsync(): Iniciando método. Usuario {0}";
                public const string SubiendoFoto = "Producto - InsertAsync(): Subiendo foto del producto. Usuario {0}";
                public const string CodigoExiste = "Producto - InsertAsync(): El Código del Producto {0} ya se encuentra registrado";
                public const string NombreExiste = "Producto - InsertAsync(): El Nombre del Producto {0} ya se encuentra registrado";
                public const string FotoCargadaCorrectamente = "Producto - InsertAsync(): Foto del producto cargada correctamente. IdFoto {0}";
                public const string UsuarioNoExiste = "Producto - InsertAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Producto - InsertAsync(): El usuario {0} creó el producto correctamente";
            }

            public struct UpdateAsync
            {
                public const string Initial = "Producto - UpdateAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Producto - UpdateAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string ProductoNoExiste = "Producto - UpdateAsync(): Producto {0} no se encuentra en la base de datos";
                public const string Finish = "Producto - UpdateAsync(): El usuario {0} actualizó la el producto correctamente";
            }

            public struct DeleteAsync
            {
                public const string Initial = "Producto - DeleteAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Producto - DeleteAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string ProductoNoExiste = "Producto - DeleteAsync(): Producto {0} no se encuentra en la base de datos";
                public const string Finish = "Producto - DeleteAsync(): El usuario {0} eliminó correctamente el producto";
            }

            public struct UpdateActivoAsync
            {
                public const string Initial = "Producto - UpdateActivoAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Producto - UpdateActivoAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string ProductoNoExiste = "Producto - UpdateActivoAsync(): Producto {0} no se encuentra en la base de datos";
                public const string Finish = "Producto - UpdateActivoAsync(): El usuario {0} Actualizó correctamente el producto";
            }
        }

        public struct Parametro
        {
            public struct UpdateDetalleParametroAsync
            {
                public const string Initial = "Parametro - UpdateDetalleParametroAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Parametro - UpdateDetalleParametroAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string DetalleParametroNoExiste = "Parametro - UpdateDetalleParametroAsync(): DetalleParametroNoExiste {0} no se encuentra en la base de datos";
                public const string Finish = "Parametro - UpdateDetalleParametroAsync(): El usuario {0} actualizó el detalle del parámetro correctamente";
            }

            public struct VistaPreviaBoletaFacturaAsync
            {
                public const string Initial = "ParametrosGenerales - VistaPreviaBoletaFacturaAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "ParametrosGenerales - VistaPreviaBoletaFacturaAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string DetalleParametroNoExiste = "ParametrosGenerales - VistaPreviaBoletaFacturaAsync(): DetalleParametroNoExiste {0} no se encuentra en la base de datos";
                public const string Finish = "ParametrosGenerales - VistaPreviaBoletaFacturaAsync(): El usuario {0} actualizó el detalle del parámetro correctamente";
            }
        }

        public struct Usuario
        {
            public struct GetAllUserByFilterAsync
            {
                public const string Initial = "Usuario - GetAllUserByFilterAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Usuario - GetAllUserByFilterAsync(): El usuario {IdUsuarioGuid} no se encuentra en la base de datos";
                public const string Finish = "Usuario - GetAllUserByFilterAsync(): El usuario {0} solicitó los usuarios correctamente";
            }

            public struct ExistCorreoAsync
            {
                public const string Initial = "ExistCorreoAsync(): Iniciando método. Correo {0}";
                public const string UsuarioExiste = "El correo {0} ya se encuentra registrado en la base de datos ";
            }

            public struct ExistNumeroDocumentoAsync
            {
                public const string Initial = "ExistNumeroDocumentoAsync(): Iniciando método. Número de documento {0}";
                public const string UsuarioExiste = "El número de documento {0} ya se encuentra registrado en la base de datos ";
            }

            public struct UpdateAsync
            {
                public const string Initial = "Usuario - UpdateAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Usuario - UpdateAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string UsuarioSeleccionadoNoExiste = "Usuario - UpdateAsync(): El usuario seleccionado {0} no se encuentra en la base de datos";
                public const string UsuarioActualizarNoExiste = "Usuario - UpdateAsync(): Usuario {id} no se encuentra en la base de datos";
                public const string Finish = "Usuario - UpdateAsync(): El usuario {0} actualizó la información del usuario correctamente";
            }

            public struct DeleteAsync
            {
                public const string Initial = "Usuario - DeleteAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Usuario - DeleteAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string UsuarioSeleccionadoNoExiste = "Usuario - DeleteAsync(): El usuario seleccionado {0} no se encuentra en la base de datos";
                public const string UsuarioActualizarNoExiste = "Usuario - DeleteAsync(): Usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Usuario - DeleteAsync(): El usuario {0} eliminó correctamente el usuario";
            }

            public struct UpdateActivoAsync
            {
                public const string Initial = "Usuario - UpdateActivoAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Usuario - UpdateActivoAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string UsuarioSeleccionadoNoExiste = "Usuario - UpdateActivoAsync(): El usuario seleccionado {0} no se encuentra en la base de datos";
                public const string UsuarioActualizarNoExiste = "Usuario - UpdateActivoAsync(): Usuario {id} no se encuentra en la base de datos";
                public const string Finish = "Usuario - UpdateActivoAsync(): El usuario {0} Actualizó correctamente el usuario";
            }
        }

        public struct Negocio
        {
            public struct UpdateNegocioAsync
            {
                public const string Initial = "Negocio - UpdateNegocioAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Negocio - UpdateNegocioAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Negocio - UpdateNegocioAsync(): El usuario {0} actualizó la información de la empresa correctamente";
            }

        }

        public struct Cliente
        {
            public struct ExistCorreoAsync
            {
                public const string Initial = "Cliente - ExistCorreoAsync(): Iniciando método. Correo {0}";
                public const string ClienteExiste = "El correo {0} ya se encuentra registrado";
            }

            public struct ExistNumeroDocumentoAsync
            {
                public const string Initial = "Cliente - ExistNumeroDocumentoAsync(): Iniciando método. Número de documento {0}";
                public const string ClienteExiste = "El número de documento {0} ya se encuentra registrado";
            }

            public struct GetAllByFilterAsync
            {
                public const string Initial = "Cliente - GetAllByFilterAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "El usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Cliente - GetAllByFilterAsync(): El usuario {0} solicitó los clientes correctamente";
            }

            public struct InsertAsync
            {
                public const string Initial = "Cliente - InsertAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Cliente - InsertAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Cliente - InsertAsync(): El usuario {0} creó el cliente correctamente";
            }

            public struct UpdateAsync
            {
                public const string Initial = "Cliente - UpdateAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Cliente - UpdateAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string ClienteNoExiste = "Cliente - UpdateAsync(): Cliente {0} no se encuentra en la base de datos";
                public const string Finish = "Cliente - UpdateAsync(): El usuario {0} actualizó la información del cliente correctamente";
            }

            public struct DeleteAsync
            {
                public const string Initial = "Cliente - DeleteAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Cliente - DeleteAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string ClienteNoExiste = "Cliente - DeleteAsync(): Cliente {0} no se encuentra en la base de datos";
                public const string Finish = "Cliente - DeleteAsync(): El usuario {0} eliminó correctamente el cliente";
            }

            public struct UpdateActivoAsync
            {
                public const string Initial = "Cliente - UpdateActivoAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Cliente - UpdateActivoAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string ClienteNoExiste = "Cliente - UpdateActivoAsync(): Cliente {0} no se encuentra en la base de datos";
                public const string Finish = "Cliente - UpdateActivoAsync(): El usuario {0} Actualizó correctamente el cliente";
            }
        }

        public struct Venta
        {
            public struct InsertAsync
            {
                public const string Initial = "Venta - InsertAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Venta - InsertAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string Finish = "Venta - InsertAsync(): El usuario {0} creó la venta correctamente";
                public const string Error = "Venta - InsertAsync(): Hubo un error al momento de registrar la venta {0}. Detalle: ";
                public const string ErrorCorrelativo = "Venta - InsertAsync(): No se encontró un correlativo para la serie especificada.";
                public const string ErrorStock = "Venta - InsertAsync(): Stock insuficiente para el producto. {0}";
            }

            public struct AnulaAsync
            {
                public const string Initial = "Venta - AnulaAsync(): Iniciando método. Usuario {0}";
                public const string UsuarioNoExiste = "Venta - AnulaAsync(): El usuario {0} no se encuentra en la base de datos";
                public const string VentaNoExiste = "Venta - AnulaAsync(): Vebta {0} no se encuentra en la base de datos";
                public const string Finish = "Venta - AnulaAsync(): El usuario {0} eliminó anuló correctamente la venta";
            }

            public struct Reporte
            {
                public const string TemplateNoEncontradoError = "El template del reporte no fue encontrado. FilePath {0}";
            }

        }

        public struct InsertCategoriaAsync
        {
            public const string Initial = "InsertCategoriaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "InsertCategoriaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string Finish = "InsertCategoriaAsync(): El usuario {0} creó la categoría correctamente";
        }

        public struct UpdateCategoriaAsync
        {
            public const string Initial = "UpdateCategoriaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "UpdateCategoriaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string CategoriaNoExiste = "UpdateCategoriaAsync(): Categoria {0} no se encuentra en la base de datos";
            public const string Finish = "UpdateCategoriaAsync(): El usuario {0} actualizó la categoría correctamente";
        }

        public struct DeleteCategoriaAsync
        {
            public const string Initial = "DeleteCategoriaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "DeleteCategoriaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string CategoriaNoExiste = "DeleteCategoriaAsync(): Categoria {0} no se encuentra en la base de datos";
            public const string Finish = "DeleteCategoriaAsync(): El usuario {0} eliminó correctamente la categoría";
            public const string ExisteProductosAsociados = "DeleteCategoriaAsync(): No se puede eliminar la categoria porque tienes gastos asociados.";
        }

        public struct UpdateActivoCategoriaAsync
        {
            public const string Initial = "UpdateActivoCategoriaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "UpdateActivoCategoriaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string CategoriaNoExiste = "UpdateActivoCategoriaAsync(): Categoria {0} no se encuentra en la base de datos";
            public const string Finish = "UpdateActivoCategoriaAsync(): El usuario {0} Actualizó correctamente la categoría";
        }

        public struct GetAllMarcaByFilterAsync
        {
            public const string Initial = "GetAllMarcaByFilterAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "GetAllMarcaByFilterAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string Finish = "GetAllMarcaByFilterAsync(): El usuario {0} solicitó las marcas correctamente";
        }

        public struct InsertMarcaAsync
        {
            public const string Initial = "InsertMarcaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "InsertMarcaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string Finish = "InsertMarcaAsync(): El usuario {0} creó la marca correctamente";
        }

        public struct UpdateMarcaAsync
        {
            public const string Initial = "UpdateMarcaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "UpdateMarcaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string MarcaNoExiste = "UpdateMarcaAsync(): Marca {0} no se encuentra en la base de datos";
            public const string Finish = "UpdateMarcaAsync(): El usuario {0} actualizó la marca correctamente";
        }

        public struct DeleteMarcaAsync
        {
            public const string Initial = "DeleteMarcaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "DeleteMarcaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string MarcaNoExiste = "DeleteMarcaAsync(): Marca {id} no se encuentra en la base de datos";
            public const string Finish = "DeleteMarcaAsync(): El usuario {0} eliminó correctamente la marca";
            public const string ExisteProductosAsociados = "DeleteMarcaAsync(): No se puede eliminar la marca porque tienes gastos asociados.";
        }

        public struct UpdateActivoMarcaAsync
        {
            public const string Initial = "UpdateActivoMarcaAsync(): Iniciando método. Usuario {0}";
            public const string UsuarioNoExiste = "UpdateActivoMarcaAsync(): El usuario {0} no se encuentra en la base de datos";
            public const string MarcaNoExiste = "UpdateActivoMarcaAsync(): Marca {id} no se encuentra en la base de datos";
            public const string Finish = "UpdateActivoMarcaAsync(): El usuario {0} Actualizó correctamente la marca";
        }

        public const string UsuarioNoAutorizado = "El usuario no se encuentra autorizado para poder acceder al sistema. Usuario {0}";
    }

    #endregion

}
