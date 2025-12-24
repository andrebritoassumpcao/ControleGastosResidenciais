using Asp.Versioning;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services;
using ControleGastosResidenciais.Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ControleGastosResidenciais.Api.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger) : ControllerBase
    {
        /// <summary>
        /// Cria uma nova transação.
        /// </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(TransactionResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] TransactionRequestDto transactionDto)
        {
            try
            {
                var result = await transactionService.CreateAsync(transactionDto);

                logger.LogInformation("Transação criada com sucesso: {Id}", result.Id);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors });
            }
            catch (ValidatorException ex)
            {
                logger.LogError(ex, "Erro de validação ao criar transação");
                return StatusCode((int)HttpStatusCode.BadRequest, new { errors = ex.Errors });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao criar transação");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
            }
        }

        /// <summary>
        /// Busca todas as transações.
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<TransactionResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(TransactionResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(TransactionResponseDto))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await transactionService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao listar pessoas");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
            }
        }

        /// <summary>
        /// Busca uma transação por ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TransactionResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(TransactionResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(TransactionResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(TransactionResponseDto))]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await transactionService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { errors = ex.Errors });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar transação");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { errors = new[] { new ErrorMessage(Resource.InternalErrorCode, Resource.InternalError) } });
            }
        }
    }
}
