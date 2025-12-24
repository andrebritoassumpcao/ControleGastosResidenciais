using ControleGastosResidenciais.Application.DTOs.Transactions;

namespace ControleGastosResidenciais.Application.Services.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateAsync(TransactionRequestDto transactionDto);
    Task<IEnumerable<TransactionResponseDto>> GetAllAsync();
    Task<TransactionResponseDto> GetByIdAsync(Guid id);
}
