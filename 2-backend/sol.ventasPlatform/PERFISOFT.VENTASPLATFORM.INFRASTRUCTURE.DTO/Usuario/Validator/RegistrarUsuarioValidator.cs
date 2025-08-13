using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Validator
{
    public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioRequest>
    {
        public RegistrarUsuarioValidator()
        {
            Log.Information("Iniciando validación Request RegistrarUsuarioRequest");

            RuleFor(x => x.DestinationTimeZoneIdRegistro)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Nombres)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cien).WithMessage(GV.PropertyNameCorrectLenght)
                .Matches(Strings.ValidNames).WithMessage(GV.PropertyNameOnlyCharacters)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .Must(NoNumbers).WithMessage(GV.PropertyNameNoNumbers);

            RuleFor(x => x.Apellidos)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cien).WithMessage(GV.PropertyNameCorrectLenght)
                .Matches(Strings.ValidNames).WithMessage(GV.PropertyNameOnlyCharacters)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .Must(NoNumbers).WithMessage(GV.PropertyNameNoNumbers);

            RuleFor(x => x.IdRol)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.IdGenero)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.IdTipoDocumento)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.NumeroDocumento)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Correo_Electronico)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .EmailAddress().WithMessage(GV.PropertyNameInvalidEmail);

            RuleFor(x => x.Fecha_Nacimiento)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha)
                .Must(fechaNacimiento => EsMayorDeEdad(fechaNacimiento)).WithMessage(GV.PropertyNameInvalidUserAge);

            RuleFor(x => x.Celular)
                .MaximumLength(Numeracion.Treinta).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Direccion)
                .MaximumLength(Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Contrasenia)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .MinimumLength(Numeracion.Doce).WithMessage(GV.PropertyNameLengthPassword)
                .MaximumLength(Numeracion.Ochenta).WithMessage(GV.PropertyNameMaxLengthPassword)
                .Matches(Strings.SecurePassword).WithMessage(GV.PropertyNameSecurePassword);

        }

        public override ValidationResult Validate(ValidationContext<RegistrarUsuarioRequest> context)
        {
            var validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                Log.Error($"Se encontraron errores en los parametros de entrada: ");

                foreach (var error in validationResult.Errors)
                {
                    Log.Error($"Propiedad '{error.PropertyName}': {error.ErrorMessage}");
                }
            }

            return validationResult;
        }

        private bool NoNumbers(string param)
        {
            return !param.Any(char.IsDigit);
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            var edad = DateTime.UtcNow.Year - fechaNacimiento.Year;

            if (DateTime.UtcNow < fechaNacimiento.AddYears(edad))
                edad--;

            return edad >= Numeracion.Dieciocho;

        }

    }
}
