using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Summaries
{
    public class SummaryByCategoryResponseDto
    {
        public List<SummaryByCategoryDto> Items { get; set; } = new();
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }
}
