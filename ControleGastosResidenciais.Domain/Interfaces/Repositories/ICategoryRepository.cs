using ControleGastosResidenciais.Domain.Entities;

namespace ControleGastosResidenciais.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task CreateCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(Guid id);
    Task<bool> DescriptionAlreadyExistis(string description);
    Task<Guid> DeleteCaregoryAsync(Category category);
}
