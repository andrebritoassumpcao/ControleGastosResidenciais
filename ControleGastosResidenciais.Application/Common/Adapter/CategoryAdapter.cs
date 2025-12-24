using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.DTOs.Categories;
using ControleGastosResidenciais.Domain.Entities;

namespace ControleGastosResidenciais.Application.Common.Adapter;

public class CategoryAdapter : ICategoryAdapter
{
    public Category ToCategory(CategoryRequestDto categoryDto)
    => new()
    {
        Id = Guid.NewGuid(),
        Description = categoryDto.Description,
        Purpose = categoryDto.Purpose
    };

    public CategoryResponseDto ToCategoryResponseDto(Category category)
    => new()
    {
        Id = category.Id,
        Description = category.Description,
        Purpose = category.Purpose
    };
}