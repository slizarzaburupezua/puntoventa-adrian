using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Validator
{
    public class RegistrarVentaValidator : AbstractValidator<RegistrarVentaRequest>
    {
        public RegistrarVentaValidator()
        {
            Log.Information("Iniciando validación Request RegistrarVentaRequest");

            RuleFor(x => x.DestinationTimeZoneIdRegistro)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.NotaAdicional)
                .MaximumLength(Numeracion.Quinientos).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.LstDetalleVenta)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.FechaRegistroVenta)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha);
        }

        public override ValidationResult Validate(ValidationContext<RegistrarVentaRequest> context)
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

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }

    }
}
