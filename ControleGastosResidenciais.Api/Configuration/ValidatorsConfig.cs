using ControleGastosResidenciais.Application.DTOs.Categories;
using ControleGastosResidenciais.Application.DTOs.Persons;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Application.Validators;
using FluentValidation;

namespace ControleGastosResidenciais.Api.Configuration;
/// <summary>
/// Métodos de extensão para registrar validators na Injeção de Dependencia.
/// </summary>
public static class ValidatorsConfig
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<PersonRequestDto>, PersonRequestDtoValidator>()
                .AddScoped<IValidator<CategoryRequestDto>, CategoryRequestDtoValidator>()
                .AddScoped<IValidator<TransactionRequestDto>, TransactionRequestDtoValidator>();

        return services;
    }
}
