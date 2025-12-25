using ControleGastosResidenciais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetTransactionByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task CreateTransactionAsync(Transaction transaction);
        Task<bool> DescriptionAlreadyExistis(string description);
        Task<IEnumerable<Transaction>> GetAllTransactionsByCategoryIdAsync(Guid categoryId);
        Task<IEnumerable<Transaction>> GetAllTransactionsByPersonIdAsync(Guid personId);
    }
}
