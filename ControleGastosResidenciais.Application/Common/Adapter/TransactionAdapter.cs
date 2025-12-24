using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Domain.Entities;

namespace ControleGastosResidenciais.Application.Common.Adapter;

public class TransactionAdapter : ITransactionAdapter
{
    public Transaction ToTransaction(TransactionRequestDto transactionDto)
    => new()
    {
        Id = new Guid(),
        CategoryId = transactionDto.CategoryId,
        PersonId = transactionDto.PersonId,
        Value = transactionDto.Value,
        Description = transactionDto.Description,
        Type = transactionDto.Type,
    };

    public TransactionResponseDto ToTransactionResponseDto(Transaction transaction)
    => new()
    {
        Id = transaction.Id,
        CategoryId = transaction.CategoryId,
        PersonId = transaction.PersonId,
        Value = transaction.Value,
        Description = transaction.Description,
        Type = transaction.Type,
    };
}
