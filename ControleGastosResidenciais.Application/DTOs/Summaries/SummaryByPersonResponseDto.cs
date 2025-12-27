using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Summaries
{
    public class SummaryByPersonResponseDto
    {
        public IEnumerable<SummaryByPersonDto> Items { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }
}
