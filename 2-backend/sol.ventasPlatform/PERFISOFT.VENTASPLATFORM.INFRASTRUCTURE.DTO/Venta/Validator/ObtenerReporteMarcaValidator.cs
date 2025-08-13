using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Validator
{
    public class ObtenerReporteMarcaValidator : AbstractValidator<ObtenerReporteMarcaRequest>
    {
        public ObtenerReporteMarcaValidator()
        {
            Log.Information("Iniciando validación Request ObtenerReporteMarcaRequest");

            RuleFor(x => x.DestinationTimeZoneId)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.FechaVentaInicio)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha);

            RuleFor(x => x.FechaVentaFin)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha);
        }

        public override ValidationResult Validate(ValidationContext<ObtenerReporteMarcaRequest> context)
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
