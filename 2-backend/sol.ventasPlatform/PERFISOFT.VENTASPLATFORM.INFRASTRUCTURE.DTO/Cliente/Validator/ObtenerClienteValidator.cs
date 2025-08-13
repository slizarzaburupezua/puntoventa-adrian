using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Cliente.Validator
{
    public class ObtenerClienteValidator : AbstractValidator<ObtenerClienteRequest>
    {
        public ObtenerClienteValidator()
        {
            Log.Information("Iniciando validación Request ObtenerClienteRequest");

            RuleFor(x => x.DestinationTimeZoneId)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Nombres)
                .MaximumLength(Numeracion.Quinientos).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Apellidos)
                .MaximumLength(Numeracion.Quinientos).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Celular)
                .MaximumLength(Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Direccion)
                .MaximumLength(Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.FechaRegistroInicio)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha);

            RuleFor(x => x.FechaRegistroFin)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha);
        }

        public override ValidationResult Validate(ValidationContext<ObtenerClienteRequest> context)
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

        private bool BeAValidDate(DateTime? date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}
