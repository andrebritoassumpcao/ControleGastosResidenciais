using ControleGastosResidenciais.Domain.Entities;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidenciais.Infrastructure.Repositories;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    public async Task<Transaction> GetTransactionByIdAsync(Guid id)
    {
        var result = await context.Transactions
            .Include(t => t.Person)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);

        return result!;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        var result = await context.Transactions
            .Include(t => t.Person)
            .Include(t => t.Category)
            .ToListAsync();

        return result;
    }

    public async Task CreateTransactionAsync(Transaction transaction)
    {
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();
    }
}