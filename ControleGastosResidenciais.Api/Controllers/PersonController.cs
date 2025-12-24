using Asp.Versioning;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Persons;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ControleGastosResidenciais.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController(IPersonService personService, ILogger<PersonController> logger) : ControllerBase
{
    /// <summary>
    /// Cria uma nova pessoa.
    /// </summary>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(PersonResponseDto))]

    public async Task<IActionResult> Create([FromBody] PersonRequestDto personDto)
    {
        try
        {
            var result = await personService.CreateAsync(personDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar pessoa");
            return StatusCode(500, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Lista todas as pessoas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(PersonResponseDto))]

    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await personService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao listar pessoas");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Busca uma pessoa por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(PersonResponseDto))]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await personService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { errors = ex.Errors });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar pessoa");
            return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
        }
    }

    /// <summary>
    /// Deleta uma pessoa.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(PersonResponseDto))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(PersonResponseDto))]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await personService.DeleteAsync(id);
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