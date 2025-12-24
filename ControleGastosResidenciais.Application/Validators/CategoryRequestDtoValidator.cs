using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Categories;
using FluentValidation;

namespace ControleGastosResidenciais.Application.Validators;

public class CategoryRequestDtoValidator : AbstractValidator<CategoryRequestDto>
{
    public CategoryRequestDtoValidator()
    {
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage(Resource.DescriptionError)
            .MaximumLength(100).WithMessage(Resource.DescriptionLengthError);

        RuleFor(c => c.Purpose)
            .IsInEnum().WithMessage(Resource.PurposeError);
    }
}
