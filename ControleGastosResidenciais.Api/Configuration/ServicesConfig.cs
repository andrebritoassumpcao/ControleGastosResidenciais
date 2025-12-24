using ControleGastosResidenciais.Application.Services;
using ControleGastosResidenciais.Application.Services.Interfaces;

namespace ControleGastosResidenciais.Api.Configuration;
/// <summary>
/// Métodos de extensão para registrar services na Injeção de Dependencia.
/// </summary>
public static class ServicesConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
