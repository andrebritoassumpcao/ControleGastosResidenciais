using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using ControleGastosResidenciais.Infrastructure.Repositories;

namespace ControleGastosResidenciais.Api.Configuration;

/// <summary>
/// Métodos de extensão para registrar repositórios na Injeção de Dependencia.
/// </summary>
public static class RepositoriesConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}