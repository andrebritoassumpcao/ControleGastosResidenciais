using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.DTOs.Summaries;
using ControleGastosResidenciais.Domain.Entities;
using ControleGastosResidenciais.Domain.Enums;

namespace ControleGastosResidenciais.Application.Common.Adapter
{
    public class SummaryAdapter : ISummaryAdapter
    {
        public SummaryByPersonDto ToSummaryByPersonDto(IEnumerable<Transaction> transactions)
        {
            // Calcula os totais de renda e despesa
            var totalIncome = transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Value);

            var totalExpense = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Value);

            // Pega o nome da pessoa do primeiro item (assumindo que todas as transações são da mesma pessoa)
            var name = transactions.FirstOrDefault()?.Person?.Name ?? string.Empty;
            // Pega o ID da pessoa do primeiro item
            var personId = transactions.FirstOrDefault()?.PersonId;
            // Cria o DTO de relatório´subtraindo no final os gastos da renda.
            var result = new SummaryByPersonDto
            {
                PersonId = personId,
                Name = name,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpense,
                Balance = totalIncome - totalExpense
            };

            return result;
        }

        public SummaryByCategoryDto ToSummaryByCategoryDto(IEnumerable<Transaction> transactions)
        {
            var totalIncome = transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Value);

            var totalExpense = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Value);

            var description = transactions.FirstOrDefault()?.Category?.Description ?? string.Empty;

            var categoryId = transactions.FirstOrDefault()?.CategoryId;

            var result =  new SummaryByCategoryDto
            {
                CategoryId = categoryId,
                Description = description,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpense,
                Balance = totalIncome - totalExpense
            };

            return result;
        }
    }
}
