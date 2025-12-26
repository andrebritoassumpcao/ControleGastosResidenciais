using ControleGastosResidenciais.Application.DTOs.Summaries;
using ControleGastosResidenciais.Domain.Entities;

namespace ControleGastosResidenciais.Application.Common.Adapter.Interface
{
    public interface ISummaryAdapter
    {
        SummaryByPersonDto ToSummaryByPersonDto(IEnumerable<Transaction> transactions);
        SummaryByCategoryDto ToSummaryByCategoryDto(IEnumerable<Transaction> transactions);
    }
}
