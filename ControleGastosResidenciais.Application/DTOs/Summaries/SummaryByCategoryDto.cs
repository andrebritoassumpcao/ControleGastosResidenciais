using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Summaries
{
    public class SummaryByCategoryDto
    {
        public Guid CategoryId { get; init; }
        public string Description { get; init; } = string.Empty;
        public decimal TotalIncome { get; init; }
        public decimal TotalExpenses { get; init; }
        public decimal Balance { get; init; }
    }
}
