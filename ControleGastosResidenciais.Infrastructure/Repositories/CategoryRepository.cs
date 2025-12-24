using ControleGastosResidenciais.Domain.Entities;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Infrastructure.Repositories
{
    /// <summary>
    /// Implementação do repositório de Category usando Entity Framework Core.
    /// </summary>
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        public async Task CreateCategoryAsync(Category category)
        {
            await context.Categories.AddAsync(category);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var result = await context.Categories
                .OrderBy(p => p.Description)
                .ToListAsync();

            return result;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            var result = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            return result!;
        }
        public async Task<bool> DescriptionAlreadyExistis(string description)
        {
            var result = await context.Categories
                .FirstOrDefaultAsync(p => p.Description == description);

            if (result is not null)
            {
                return true;
            }

            return false;
        }
    }
}
