using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Validators
{
    public class TransactionRequestDtoValidator : AbstractValidator<TransactionRequestDto>
    {
        public TransactionRequestDtoValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage(Resource.DescriptionError)
                .MaximumLength(100).WithMessage(Resource.DescriptionLengthError);

            RuleFor(t => t.Value)
                .GreaterThan(0).WithMessage(Resource.EmptyValueError);

            RuleFor(t => t.Type)
                .IsInEnum().WithMessage(Resource.TransactionTypeError);

            RuleFor(t => t.PersonId)
                .NotEmpty().WithMessage(Resource.EmptyPersonIdError);

            RuleFor(t => t.CategoryId)
                .NotEmpty().WithMessage(Resource.EmptyCategoryIdError);
        }
    }
}
