using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.DTOs.Categories;
using ControleGastosResidenciais.Application.Services.Interfaces;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ControleGastosResidenciais.Application.Services;

public class CategoryService(
        ICategoryRepository categoryRepository,
        IValidator<CategoryRequestDto> validator,
        ILogger<CategoryService> logger,
        ICategoryAdapter adapter) : ICategoryService
{
    /// <summary>
    /// Cria ua nova categoria após validar os dados.
    /// </summary>
    public async Task<CategoryResponseDto> CreateAsync(CategoryRequestDto categoryDto)
    {
        logger.LogInformation("Criando categoria: {Description}", categoryDto.Description);

        var validationResult = await validator.ValidateAsync(categoryDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors;
            throw new ValidationException(errors);
        }

        var category = adapter.ToCategory(categoryDto);

        await categoryRepository.CreateCategoryAsync(category);

        logger.LogInformation("Categoria criada com sucesso: {Id}", category.Id);

        var result = adapter.ToCategoryResponseDto(category);
        return result;
    }

    /// <summary>
    /// Busca todas as categorias.
    /// </summary>
    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllCategoriesAsync();
        var result = categories
            .Select(adapter.ToCategoryResponseDto)
            .ToList();

        return result;
    }
}