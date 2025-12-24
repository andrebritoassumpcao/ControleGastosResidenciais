using ControleGastosResidenciais.Application.DTOs.Categories;
using ControleGastosResidenciais.Domain.Entities;

namespace ControleGastosResidenciais.Application.Common.Adapter.Interface
{
    public interface ICategoryAdapter
    {
        Category ToCategory(CategoryRequestDto dto);
        CategoryResponseDto ToCategoryResponseDto(Category category);
    }
}
