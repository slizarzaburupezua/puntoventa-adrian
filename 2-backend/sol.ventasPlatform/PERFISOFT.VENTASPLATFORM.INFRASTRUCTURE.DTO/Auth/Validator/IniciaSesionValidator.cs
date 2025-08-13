using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Auth.Validator
{
    public class IniciaSesionValidator : AbstractValidator<IniciaSesionRequest>
    {
        public IniciaSesionValidator()
        {
            Log.Information("Iniciando validación Request UsuarioIniciaSesionRequest");

            RuleFor(x => x.Correo)
                .EmailAddress().WithMessage(GV.PropertyNameInvalidEmail)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .MinimumLength(Numeracion.Doce).WithMessage(GV.PropertyNameMinLengthUser)
                .MaximumLength(Numeracion.Cuarenta).WithMessage(GV.PropertyNameMaxLengthUser);

            RuleFor(x => x.Clave)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .MinimumLength(Numeracion.Doce).WithMessage(GV.PropertyNameLengthPassword)
                .MaximumLength(Numeracion.Ochenta).WithMessage(GV.PropertyNameMaxLengthPassword)
                .Matches(Strings.SecurePassword).WithMessage(GV.PropertyNameSecurePassword);
        }

        public override ValidationResult Validate(ValidationContext<IniciaSesionRequest> context)
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
