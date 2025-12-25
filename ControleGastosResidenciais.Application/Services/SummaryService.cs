using ControleGastosResidenciais.Application.DTOs.Summaries;
using ControleGastosResidenciais.Domain.Entities;
using ControleGastosResidenciais.Domain.Enums;
using ControleGastosResidenciais.Application.Services.Interfaces;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Services;

public class SummaryService(ITransactionRepository transactionRepository) : ISummaryService
{
    /// <summary>
    /// Gera um relatório das transações agrupadas por pessoa
    /// </summary>

    public async Task<SummaryByPersonDto> GetSummaryByPersonAsync(Guid personId)
    {
        var transactions = await transactionRepository.GetAllTransactionsByPersonIdAsync(personId);

        var totalIncome = transactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Value);

        var totalExpense = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Value);

        var name = transactions.FirstOrDefault()?.Person?.Name ?? string.Empty;

        return new SummaryByPersonDto
        {
            PersonId = personId,
            Name = name,
            TotalIncome = totalIncome,
            TotalExpenses = totalExpense,
            Balance = totalIncome - totalExpense
        };
    }

    public async Task<SummaryByPersonResponseDto> GetAllSummaryByPersonAsync()
    {
        // 1º passo: obter todas as transações
        var transactions = await transactionRepository.GetAllTransactionsAsync();
        
        // 2º passo: agrupar por pessoa
        var grouped = transactions
            .GroupBy(t => new { t.PersonId, t.Person!.Name })
            .Select(g =>
            {
                // 3º passo: calcular totais
                var totalIncome = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value);
                var totalExpense = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Value);
                // 4º passo: criar DTO
                return new SummaryByPersonDto
                {
                    PersonId = g.Key.PersonId,
                    Name = g.Key.Name,
                    TotalIncome = totalIncome,
                    TotalExpenses = totalExpense,
                    Balance = totalIncome - totalExpense
                };
            })
            .OrderBy(x => x.Name)
            .ToList();
        // 5º passo: criar resposta com totais gerais
        var response = new SummaryByPersonResponseDto
        {
            Items = grouped,
            TotalIncome = grouped.Sum(x => x.TotalIncome),
            TotalExpense = grouped.Sum(x => x.TotalExpenses)
        };

        return response;
    }

    /// <summary>
    // Mesma lógica do relatório por pessoa, mas agrupando por categoria
    /// </summary>
    public async Task<SummaryByCategoryResponseDto> GetSummaryByCategoryAsync()
    {
        var transactions = await transactionRepository.GetAllTransactionsAsync();

        var grouped = transactions
            .GroupBy(t => new { t.CategoryId, t.Category!.Description })
            .Select(g =>
            {
                var totalIncome = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value);
                var totalExpense = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Value);

                return new SummaryByCategoryDto
                {
                    CategoryId = g.Key.CategoryId,
                    Description = g.Key.Description,
                    TotalIncome = totalIncome,
                    TotalExpenses = totalExpense,
                    Balance = totalIncome - totalExpense
                };
            })
            .OrderBy(x => x.Description)
            .ToList();

        var response = new SummaryByCategoryResponseDto
        {
            Items = grouped,
            TotalIncome = grouped.Sum(x => x.TotalIncome),
            TotalExpense = grouped.Sum(x => x.TotalExpenses)
        };

        return response;
    }
}
