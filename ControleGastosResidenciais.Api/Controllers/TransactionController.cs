using Asp.Versioning;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Transactions;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services.Interfaces;
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
            logger.LogInformation("Recebendo requisição para criar transação: {Description}", transactionDto.Description);

            var result = await transactionService.CreateAsync(transactionDto);

            logger.LogInformation("Transação criada com sucesso: {Id}", result.Id);

            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
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
            logger.LogInformation("Recebendo requisição para buscar todas as transações");

            var result = await transactionService.GetAllAsync();

            logger.LogInformation($"Retornando {result.Count()} transações");

            return Ok(result);
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
