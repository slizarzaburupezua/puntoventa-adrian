using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Validator
{
    public class ExistNumeroDocumentoClienteValidator : AbstractValidator<ExistNumeroDocumentoClienteRequest>
    {
        public ExistNumeroDocumentoClienteValidator()
        {
            Log.Information("Iniciando validación Request ExistNumeroDocumentoClienteRequest");

            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.NumeroDocumento)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

        }

        public override ValidationResult Validate(ValidationContext<ExistNumeroDocumentoClienteRequest> context)
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
