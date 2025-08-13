using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Usuario.Validator
{
    public class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioRequest>
    {
        public ActualizarUsuarioValidator()
        {
            Log.Information("Iniciando validación Request ActualizarUsuarioRequest");

            RuleFor(x => x.DestinationTimeZoneIdActualizacion)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdRol)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.Celular)
                .MaximumLength(Numeracion.Treinta).WithMessage(GV.PropertyNameCorrectLenght);

            RuleFor(x => x.Direccion)
                .MaximumLength(Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght);

        }

        public override ValidationResult Validate(ValidationContext<ActualizarUsuarioRequest> context)
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

        private bool NoNumbers(string param)
        {
            return !param.Any(char.IsDigit);
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            var edad = DateTime.UtcNow.Year - fechaNacimiento.Year;

            if (DateTime.UtcNow < fechaNacimiento.AddYears(edad))
                edad--;

            return edad >= Numeracion.Dieciocho;

        }

    }
}
