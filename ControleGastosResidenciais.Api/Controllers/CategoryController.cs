using Asp.Versioning;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Categories;
using ControleGastosResidenciais.Application.DTOs.Persons;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services;
using ControleGastosResidenciais.Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ControleGastosResidenciais.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) : ControllerBase
{
    /// <summary>
    /// Cria uma nova pessoa.x
    /// </summary>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(CategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(CategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CategoryResponseDto))]
    public async Task<IActionResult> Create([FromBody] CategoryRequestDto categoryDto)
    {
        try
        {
            var result = await categoryService.CreateAsync(categoryDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors });
        }
        catch (ValidatorException ex)
        {
            logger.LogError(ex, "Erro de validação ao criar Categorias");
            return StatusCode((int)HttpStatusCode.BadRequest, new { errors = ex.Errors });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar pessoa");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Busca todas as categorias.
    /// </summary>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<CategoryResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(CategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CategoryResponseDto))]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Recebendo requisição para buscar todas as categorias");

        var result = await categoryService.GetAllAsync();

        logger.LogInformation($"Retornando {result.Count()} categorias");

        return Ok(result);
    }

    /// <summary>
    /// Busca uma categoria por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(CategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CategoryResponseDto))]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await categoryService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { errors = ex.Errors });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar categoria");
            return StatusCode(500, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Deleta uma pessoa.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Guid))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Guid))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(Guid))]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await categoryService.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { errors = ex.Errors });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao deletar pessoa");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }
}
