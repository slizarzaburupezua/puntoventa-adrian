using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Validator
{
    public class NotifyOlvideContraseniaValidator : AbstractValidator<NotifyOlvideContraseniaRequest>
    {
        public NotifyOlvideContraseniaValidator()
        {
            Log.Information("Iniciando validación Request NotifyOlvideContraseniaRequest");

            RuleFor(x => x.DestinationTimeZone)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Correo)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .EmailAddress().WithMessage(GV.PropertyNameInvalidEmail)
                .MinimumLength(Numeracion.Cinco).WithMessage(GV.PropertyNameInvalidLength);
            ;
        }

        public override ValidationResult Validate(ValidationContext<NotifyOlvideContraseniaRequest> context)
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
