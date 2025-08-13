using FluentValidation;
using FluentValidation.Results;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Structure;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Request;
using Serilog;
using GV = PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.COMMON.Resource.Validator;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.DTO.Inventario.Categoria.Validator
{
    public class RegistrarCategoriaValidator : AbstractValidator<RegistrarCategoriaRequest>
    {
        public RegistrarCategoriaValidator()
        {
            Log.Information("Iniciando validación Request RegistrarCategoriaRequest");

            RuleFor(x => x.DestinationTimeZoneIdRegistro)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.IdUsuarioGuid)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Id_Medida)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty)
                .NotEqual(Numeracion.Cero).WithMessage(GV.PropertyNameCorrectValue);

            RuleFor(x => x.Nombre)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Doscientos).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Color)
                .NotNull().WithMessage(GV.PropertyNameNotNull)
                .Length(Numeracion.Dos, Numeracion.Cincuenta).WithMessage(GV.PropertyNameCorrectLenght)
                .NotEmpty().WithMessage(GV.PropertyNameNotEmpty);

            RuleFor(x => x.Descripcion)
                .MaximumLength(Numeracion.Quinientos).WithMessage(GV.PropertyNameCorrectLenght);
        }

        public override ValidationResult Validate(ValidationContext<RegistrarCategoriaRequest> context)
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
