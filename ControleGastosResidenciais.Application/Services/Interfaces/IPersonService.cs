using ControleGastosResidenciais.Application.DTOs.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonResponseDto> CreateAsync(PersonRequestDto dto);
        Task<List<PersonResponseDto>> GetAllAsync();
        Task<PersonResponseDto> GetByIdAsync(Guid id);
        Task<Guid> DeleteAsync(Guid id);

    }
}
