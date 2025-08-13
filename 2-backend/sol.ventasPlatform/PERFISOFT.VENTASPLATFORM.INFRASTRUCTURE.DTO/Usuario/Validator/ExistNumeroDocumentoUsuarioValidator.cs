using FluentValidation;
using FluentValidation.Results;

using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Validator
{
    public class ExistNumeroDocumentoUsuarioValidator : AbstractValidator<ExistNumeroDocumentoUsuarioRequest>
    {
        public ExistNumeroDocumentoUsuarioValidator()
        {
            Log.Information("Iniciando validación Request ExistNumeroDocumentoUsuarioRequest");

            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.NumeroDocumento)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

        }

        public override ValidationResult Validate(ValidationContext<ExistNumeroDocumentoUsuarioRequest> context)
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
