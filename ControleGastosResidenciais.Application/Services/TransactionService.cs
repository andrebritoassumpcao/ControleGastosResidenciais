using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services.Interfaces;
using ControleGastosResidenciais.Domain.Entities;
using ControleGastosResidenciais.Domain.Enums;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ControleGastosResidenciais.Application.Services;

public class TransactionService(
        ITransactionRepository transactionRepository,
        IPersonRepository personRepository,
        ICategoryRepository categoryRepository,
        IValidator<TransactionRequestDto> validator,
        ILogger<TransactionService> logger,
        ITransactionAdapter adapter
        ) : ITransactionService
{
    public async Task<TransactionResponseDto> CreateAsync(TransactionRequestDto transactionDto)
    {
        logger.LogInformation("Criando transação para PersonId {PersonId}, CategoryId {CategoryId}", transactionDto.PersonId, transactionDto.CategoryId);

        // 1º. Validar requisição
        var validationResult = await validator.ValidateAsync(transactionDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        // 2º. Carregar entidades existentes para validação
        var personExists = await personRepository.GetPersonByIdAsync(transactionDto.PersonId)
                     ?? throw new NotFoundException(Resource.PersonNotFoundCode, Resource.PersonNotFound);

        var categoryExists = await categoryRepository.GetCategoryByIdAsync(transactionDto.CategoryId)
                       ?? throw new NotFoundException(Resource.CategoryNotFoundCode, Resource.CategoryNotFound);

        // 3º Mapear dto para entidade
        var transaction = adapter.ToTransaction(transactionDto);

        // 4º. Implementar regras de negócio
        ValidateNewTransaction(transaction);

        // 5º.Persistencia no banco de dados
        await transactionRepository.CreateTransactionAsync(transaction);

        logger.LogInformation("Transação criada com sucesso: {Id}", transaction.Id);

        // 6. Retornar DTO como resposta
        var result = adapter.ToTransactionResponseDto(transaction);
        return result;
    }

    public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync()
    {
        var transactions = await transactionRepository.GetAllTransactionsAsync();
        return transactions.Select(adapter.ToTransactionResponseDto).ToList();
    }

    public async Task<TransactionResponseDto> GetByIdAsync(Guid id)
    {
        var result = await transactionRepository.GetTransactionByIdAsync(id);

        if (result is null)
            throw new ValidatorException(Resource.TransactionInvalidCategoryCode, Resource.TransactionNotFound);

        return adapter.ToTransactionResponseDto(result)!;
    }

    private void ValidateNewTransaction(Transaction transaction)
    {
        // Regra 1: menor de idade só pode ecolher TransactionType = Expenses(despesa)
        if (transaction.Person.Age < 18 && transaction.Type == TransactionType.Income)
        {
            throw new ValidatorException(
                Resource.TransactionMinorIncomeNotAllowedCode,
                Resource.TransactionMinorIncomeNotAllowed);
        }

        // Regra 2: Não é possivel ter tipo e finalidade diferentes
        if (transaction.Type == TransactionType.Expense &&
            transaction.Category.Purpose == CategoryPurpose.Income)
        {
            throw new ValidatorException(
                Resource.TransactionInvalidCategoryCode,
                Resource.TransactionInvalidCategory);
        }

        if (transaction.Type == TransactionType.Income &&
            transaction.Category.Purpose == CategoryPurpose.Expense)
        {
            throw new ValidatorException(
                Resource.TransactionInvalidCategoryCode,
                Resource.TransactionInvalidCategory);
        }
    }
}
