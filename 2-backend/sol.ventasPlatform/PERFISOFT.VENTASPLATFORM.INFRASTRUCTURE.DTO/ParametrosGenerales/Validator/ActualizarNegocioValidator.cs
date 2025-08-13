using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.ParametrosGenerales.Validator
{
    public class ActualizarNegocioValidator : AbstractValidator<ActualizarNegocioRequest>
    {
        public ActualizarNegocioValidator()
        {
            Log.Information("Iniciando validación Request ActualizarNegocioRequest");

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.DestinationTimeZoneIdActualizacion)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdMoneda)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.RazonSocial)
                .MaximumLength(Numeracion.Treinta).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Ruc)
                .MaximumLength(Numeracion.Treinta).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Direccion)
                .MaximumLength(Numeracion.Quinientos).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Celular)
                .MaximumLength(Numeracion.Treinta).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Correo)
                .EmailAddress().WithMessage(GV.PropertyNameInvalidEmail)
                .MaximumLength(Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght);
 
            RuleFor(x => x.CodFormatoImpresion)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .Must(x => x == Parametros.FormatoImpresion.TICKETERA || x == Parametros.FormatoImpresion.PDF).WithMessage(GV.PropertyNameCorrectValue);
        }

        public override ValidationResult Validate(ValidationContext<ActualizarNegocioRequest> context)
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
