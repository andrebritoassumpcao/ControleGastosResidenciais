using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Categories;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Application.Exceptions;
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

        bool categoryExists = await categoryRepository.DescriptionAlreadyExistis(categoryDto.Description);
        if (categoryExists)
        {
            throw new ValidatorException(Resource.CategoryAlreadyExistisCode, Resource.CategoryAlreadyExistis);
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
    /// <summary>
    /// Busca categoria pelo id.
    /// </summary>
    public async Task<CategoryResponseDto> GetByIdAsync(Guid id)
    {
        var result = await categoryRepository.GetCategoryByIdAsync(id);

        if (result is null)
            throw new ValidatorException(Resource.TransactionInvalidCategoryCode, Resource.TransactionNotFound);

        return adapter.ToCategoryResponseDto(result);
    }

    /// <summary>
    /// Deleta a categoria e retorna o Id.
    /// </summary>
    public async Task<Guid> DeleteAsync(Guid id)
    {
        var person = await categoryRepository.GetCategoryByIdAsync(id);

        if (person is null)
        {
            throw new NotFoundException(Resource.PersonNotFoundCode, Resource.PersonNotFoundCode);
        }

        await categoryRepository.DeleteCaregoryAsync(person);

        logger.LogInformation("Pessoa deletada com sucesso: {Id}", id);

        return person.Id;
    }
}