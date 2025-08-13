using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Validator
{
    public class RegistrarClienteValidator : AbstractValidator<RegistrarClienteRequest>
    {
        public RegistrarClienteValidator()
        {
            Log.Information("Iniciando validación Request RegistrarClienteRequest");

            RuleFor(x => x.DestinationTimeZoneIdRegistro)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdTipoDocumento)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.IdGenero)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.NumeroDocumento)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Nombres)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Apellidos)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.CorreoElectronico)
                .EmailAddress().WithMessage(GV.PropertyNameInvalidEmail)
                .MaximumLength(Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Celular)
                .MaximumLength(Numeracion.Treinta).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Direccion)
                .MaximumLength(Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght);
        }

        public override ValidationResult Validate(ValidationContext<RegistrarClienteRequest> context)
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
    }
}
