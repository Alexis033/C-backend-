using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerUpdateValidator: AbstractValidator<BeerUpdateDTO>
    {
        public BeerUpdateValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El id es necesario");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe tener entre 2 y 20 caracteres");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage("Error en el valor de la marca");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");
        }
    }
}
