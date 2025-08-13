using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Validator
{
    public class ObtenerUsuarioValidator : AbstractValidator<ObtenerUsuarioRequest>
    {

        public ObtenerUsuarioValidator()
        {
            Log.Information("Iniciando validación Request ObtenerUsuarioRequest");

            RuleFor(x => x.DestinationTimeZoneId)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuario)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.FechaRegistroInicio)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha);

            RuleFor(x => x.FechaRegistroFin)
                .Must(BeAValidDate).WithMessage(GV.PropertyNameInvalidFecha);
        }


        public override ValidationResult Validate(ValidationContext<ObtenerUsuarioRequest> context)
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
