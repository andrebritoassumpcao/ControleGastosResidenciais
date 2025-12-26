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
        Task<SummaryByPersonDto> GetSummaryByPersonAsync(Guid personId);
        Task<SummaryByPersonResponseDto> GetAllSummaryByPersonAsync();
        Task<SummaryByCategoryDto> GetSummaryByCategoryAsync(Guid categoryId);
        Task<SummaryByCategoryResponseDto> GetAllSummaryByCategoryAsync();
    }
}
