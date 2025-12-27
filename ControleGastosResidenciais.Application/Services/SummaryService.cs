using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Summaries;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services.Interfaces;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;

namespace ControleGastosResidenciais.Application.Services;

public class SummaryService(ITransactionRepository transactionRepository, IPersonRepository personRepository, ICategoryRepository categoryRepository, ISummaryAdapter adapter) : ISummaryService
{
    /// <summary>
    /// Gera um relatório das transações agrupadas por pessoa
    /// </summary>

    public async Task<SummaryByPersonDto> GetSummaryByPersonAsync(Guid personId)
    {
        if (personRepository.GetPersonByIdAsync(personId) == null)
        {
            throw new NotFoundException(Resource.PersonNotFoundCode, Resource.PersonNotFound);
        }
        var transactions = await transactionRepository.GetAllTransactionsByPersonIdAsync(personId);
        // usar o adapter para converter a lista de transações em um SummaryByPersonDto
        var result = adapter.ToSummaryByPersonDto(transactions);

        return result;
    }

    public async Task<SummaryByPersonResponseDto> GetAllSummaryByPersonAsync()
    {
        var transactions = await transactionRepository.GetAllTransactionsAsync();

        // Agrupa por PersonId e converte cada grupo usando o adapter
        var grouped = transactions
            .GroupBy(t => t.PersonId)
            .Select(group => adapter.ToSummaryByPersonDto(group))
            .OrderBy(x => x.Name)
            .ToList();

        // Cria o objeto de resposta com os totais gerais
        var result = new SummaryByPersonResponseDto
        {
            Items = grouped,
            TotalIncome = grouped.Sum(x => x.TotalIncome),
            TotalExpense = grouped.Sum(x => x.TotalExpenses)
        };

        return result;
    }

    /// <summary>
    // Mesma lógica do relatório por pessoa, mas agrupando por categoria
    /// </summary>
    public async Task<SummaryByCategoryDto> GetSummaryByCategoryAsync(Guid categoryId)
    {
        if (categoryRepository.GetCategoryByIdAsync(categoryId) == null)
        {
            throw new NotFoundException(Resource.CategoryNotFoundCode, Resource.CategoryNotFound);
        }
        var transactions = await transactionRepository.GetAllTransactionsByCategoryIdAsync(categoryId);
        // usar o adapter para converter a lista de transações em um SummaryByCategoryDto
        var result = adapter.ToSummaryByCategoryDto(transactions);

        return result;
    }

    public async Task<SummaryByCategoryResponseDto> GetAllSummaryByCategoryAsync()
    {
        var transactions = await transactionRepository.GetAllTransactionsAsync();

        var grouped = transactions
             .GroupBy(t => t.CategoryId)
             .Select(group => adapter.ToSummaryByCategoryDto(group))
             .OrderBy(x => x.Description)
             .ToList();

        var response = new SummaryByCategoryResponseDto
        {
            Items = grouped,
            TotalIncome = grouped.Sum(x => x.TotalIncome),
            TotalExpense = grouped.Sum(x => x.TotalExpenses),
        };

        return response;
    }
}
