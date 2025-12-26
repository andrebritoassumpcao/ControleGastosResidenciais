using Asp.Versioning;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Summaries;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services;
using ControleGastosResidenciais.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ControleGastosResidenciais.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class SummaryController(ISummaryService summaryService, ILogger<SummaryController> logger) : ControllerBase
{

    /// <summary>
    /// Busca todos os relatórios das pessoas cadastradas.
    /// </summary>
    [HttpGet("person-summary")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SummaryByPersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SummaryByPersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SummaryByPersonResponseDto))]
    public async Task<IActionResult> GetAllSummaryByPerson()
    {
        try
        {
            var result = await summaryService.GetAllSummaryByPersonAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao listar relatórios por pessoas");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Busca o relatório de uma pessoa por ID.
    /// </summary>
    [HttpGet("person-summary/{personId}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SummaryByPersonDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SummaryByPersonDto))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(SummaryByPersonDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SummaryByPersonDto))]
    public async Task<IActionResult> GetSummaryByPersonId([FromRoute] Guid personId)
    {
        try
        {
            var result = await summaryService.GetSummaryByPersonAsync(personId);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { errors = ex.Errors });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar relatório pelo person id");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Busca todos os relatórios das categorias cadastradas.
    /// </summary>
    [HttpGet("category-summary")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SummaryByCategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SummaryByCategoryResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SummaryByCategoryResponseDto))]
    public async Task<IActionResult> GetAllSummaryByCategory()
    {
        try
        {
            var result = await summaryService.GetAllSummaryByCategoryAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao listar relatórios por pessoas");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Busca o relatório de uma categoria por ID.
    /// </summary>
    [HttpGet("category-summary/{categoryId}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SummaryByCategoryDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(SummaryByCategoryDto))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(SummaryByCategoryDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(SummaryByCategoryDto))]
    public async Task<IActionResult> GetById([FromRoute] Guid categoryId)
    {
        try
        {
            var result = await summaryService.GetSummaryByCategoryAsync(categoryId);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { errors = ex.Errors });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar relatório pelo person id");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }
}
