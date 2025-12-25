using ControleGastosResidenciais.Application.DTOs.Summaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<SummaryByPersonResponseDto> GetSummaryByPersonAsync();
        Task<SummaryByCategoryResponseDto> GetSummaryByCategoryAsync();
    }
}
