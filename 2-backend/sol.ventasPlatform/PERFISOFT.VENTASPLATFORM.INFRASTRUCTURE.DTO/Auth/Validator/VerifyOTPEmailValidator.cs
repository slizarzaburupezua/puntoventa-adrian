using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Validator
{
    public class VerifyOTPEmailValidator : AbstractValidator<VerifyOTPEmailRequest>
    {
        public VerifyOTPEmailValidator()
        {
            Log.Information("Iniciando validación Request VerifyOTPEmailRequest");

            RuleFor(x => x.DestinationTimeZone)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Codigo)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .MaximumLength(Numeracion.Diez).WithMessage(GV.PropertyNameInvalidLengthOTP)
                .MinimumLength(Numeracion.Dos).WithMessage(GV.PropertyNameInvalidLength);

            RuleFor(x => x.Correo)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .EmailAddress().WithMessage(GV.PropertyNameInvalidEmail)
                .MinimumLength(Numeracion.Dos).WithMessage(GV.PropertyNameInvalidLength);
        }

        public override ValidationResult Validate(ValidationContext<VerifyOTPEmailRequest> context)
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
