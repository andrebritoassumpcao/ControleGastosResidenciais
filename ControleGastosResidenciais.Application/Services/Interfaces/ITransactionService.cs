using ControleGastosResidenciais.Application.DTOs.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponseDto> CreateAsync(TransactionRequestDto transactionDto);
        Task<IEnumerable<TransactionResponseDto>> GetAllAsync();
    }
}
