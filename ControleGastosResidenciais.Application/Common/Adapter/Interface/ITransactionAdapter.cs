using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Domain.Entities;

namespace ControleGastosResidenciais.Application.Common.Adapter.Interface;

public interface ITransactionAdapter
{
    Transaction ToTransaction(TransactionRequestDto transactionDto);

    TransactionResponseDto ToTransactionResponseDto(Transaction transaction);
}
