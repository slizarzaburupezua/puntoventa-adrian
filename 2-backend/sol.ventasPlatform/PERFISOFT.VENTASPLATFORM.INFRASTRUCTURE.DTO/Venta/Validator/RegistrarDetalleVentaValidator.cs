using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Venta.Validator
{
    public class RegistrarDetalleVentaValidator : AbstractValidator<RegistrarDetalleVentaRequest>
    {
        public RegistrarDetalleVentaValidator()
        {
            Log.Information("Iniciando validación Request RegistrarDetalleVentaRequest");

            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.Cantidad)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.NombreProducto)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.ColorProducto)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.NombreCategoria)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.ColorCategoria)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.NombreMarca)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.ColorMarca)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.PrecioVenta)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);
        }

        public override ValidationResult Validate(ValidationContext<RegistrarDetalleVentaRequest> context)
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
