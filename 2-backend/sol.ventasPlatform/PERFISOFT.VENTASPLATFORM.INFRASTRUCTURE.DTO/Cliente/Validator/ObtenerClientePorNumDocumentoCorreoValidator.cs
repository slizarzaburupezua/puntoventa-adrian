using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Validator
{
    public class ObtenerClientePorNumDocumentoCorreoValidator : AbstractValidator<ObtenerClientePorNumDocumentoCorreoRequest>
    {
        public ObtenerClientePorNumDocumentoCorreoValidator()
        {
            Log.Information("Iniciando validación Request ObtenerClientePorNumDocumentoCorreoRequest");

            RuleFor(x => x.Parametro)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
            .MinimumLength(Numeracion.Tres).WithMessage(GV.PropertyNameInvalidLength)
            .MaximumLength(Numeracion.Cincuenta).WithMessage(GV.PropertyNameInvalidLength);

            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

        }

        public override ValidationResult Validate(ValidationContext<ObtenerClientePorNumDocumentoCorreoRequest> context)
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
