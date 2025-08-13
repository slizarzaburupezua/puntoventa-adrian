using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Validator
{
    public class ActualizarContraseniaValidator : AbstractValidator<ActualizarContraseniaRequest>
    {
        public ActualizarContraseniaValidator()
        {
            Log.Information("Iniciando validación Request ActualizarContraseniaRequest");

            RuleFor(x => x.DestinationTimeZoneIdActualizacion)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.ContraseniaActual)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .MinimumLength(Numeracion.Doce).WithMessage(GV.PropertyNameLengthPassword)
                .MaximumLength(Numeracion.Ochenta).WithMessage(GV.PropertyNameMaxLengthPassword)
                .Matches(Strings.SecurePassword).WithMessage(GV.PropertyNameSecurePassword);

            RuleFor(x => x.ContraseniaNueva)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .MinimumLength(Numeracion.Doce).WithMessage(GV.PropertyNameLengthPassword)
                .MaximumLength(Numeracion.Ochenta).WithMessage(GV.PropertyNameMaxLengthPassword)
                .Matches(Strings.SecurePassword).WithMessage(GV.PropertyNameSecurePassword);
        }

        public override ValidationResult Validate(ValidationContext<ActualizarContraseniaRequest> context)
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
