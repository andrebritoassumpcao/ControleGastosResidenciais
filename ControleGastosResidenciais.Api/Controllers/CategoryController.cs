using Asp.Versioning;
using ControleGastosResidenciais.Application.DTOs.Categories;
using ControleGastosResidenciais.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ControleGastosResidenciais.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) : ControllerBase
{
    /// <summary>
    /// Cria uma nova pessoa.
    /// </summary>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(CategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CategoryRequestDto categoryDto)
    {
        logger.LogInformation("Recebendo requisição para criar categoria: {Description}", categoryDto.Description);

        var result = await categoryService.CreateAsync(categoryDto);

        logger.LogInformation("Categoria criada com sucesso: {Id}", result.Id);

        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    /// <summary>
    /// Busca todas as categorias.
    /// </summary>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<CategoryResponseDto>))]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Recebendo requisição para buscar todas as categorias");

        var result = await categoryService.GetAllAsync();

        logger.LogInformation($"Retornando {result.Count()} categorias");

        return Ok(result);
    }
}
