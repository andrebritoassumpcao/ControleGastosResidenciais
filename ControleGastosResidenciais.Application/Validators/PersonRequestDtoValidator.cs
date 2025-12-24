using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Persons;
using FluentValidation;

namespace ControleGastosResidenciais.Application.Validators
{
    public class PersonRequestDtoValidator : AbstractValidator<PersonRequestDto>
    {
        public PersonRequestDtoValidator() 
        {
            this.RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(Resource.EmptyNameError);

            this.RuleFor(request => request.Age)
               .NotEmpty()
               .WithMessage(Resource.EmptyNameError);
        }
    }
}
