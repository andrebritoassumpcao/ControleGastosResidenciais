using ControleGastosResidenciais.Application.DTOs.Summaries;
using ControleGastosResidenciais.Domain.Entities;
using ControleGastosResidenciais.Domain.Enums;

namespace ControleGastosResidenciais.Application.Common.Adapter
{
    public class SummaryAdapter : ISummaryAdapter
    {
        public SummaryByPersonDto ToSummaryByPersonDto(IEnumerable<Transaction> transactions)
        {
            var totalIncome = transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Value);

            var totalExpense = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Value);

            var name = transactions.FirstOrDefault()?.Person?.Name ?? string.Empty;

            var personId = transactions.FirstOrDefault()?.PersonId;

            return new SummaryByPersonDto
            {
                PersonId = personId,
                Name = name,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpense,
                Balance = totalIncome - totalExpense
            };
        }

        public SummaryByPersonDto ToSummaryByPersonDto(IGrouping<(Guid PersonId, string Name), Transaction> group)
        {
            var totalIncome = group
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Value);

            var totalExpense = group
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Value);

            return new SummaryByPersonDto
            {
                PersonId = group.Key.PersonId,
                Name = group.Key.Name,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpense,
                Balance = totalIncome - totalExpense
            };
        }
    }
}
