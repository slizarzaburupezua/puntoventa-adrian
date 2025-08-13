using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Producto.Validator
{
    public class ActualizarActivoProductoValidator : AbstractValidator<ActualizarActivoProductoRequest>
    {
        public ActualizarActivoProductoValidator()
        {
            Log.Information("Iniciando validación Request ActualizarActivoProductoValidator");

            RuleFor(x => x.DestinationTimeZoneIdActualizacion)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Id)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.Activo)
                .NotNull().WithMessage(GV.PropertyNameNotNull);
        }

        public override ValidationResult Validate(ValidationContext<ActualizarActivoProductoRequest> context)
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
