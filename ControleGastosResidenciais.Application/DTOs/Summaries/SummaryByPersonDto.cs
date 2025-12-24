using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Summaries
{
    public class SummaryByPersonDto
    {
        public Guid PersonId { get; init; }

        public string Name { get; init; } = string.Empty;

        public decimal TotalIncome{ get; init; }

        public decimal TotalExpenses { get; init; }

        public decimal Balance { get; init; }
    }
}
