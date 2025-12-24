using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.Common.Resources;
using ControleGastosResidenciais.Application.DTOs.Persons;
using ControleGastosResidenciais.Application.Exceptions;
using ControleGastosResidenciais.Application.Services.Interfaces;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ControleGastosResidenciais.Application.Services;

public class PersonService(
        IPersonRepository personRepository,
        IValidator<PersonRequestDto> validator,
        ILogger<PersonService> logger,
        IPersonAdapter adapter) : IPersonService
{
    /// <summary>
    /// Cria uma nova pessoa após validar os dados.
    /// </summary>
    public async Task<PersonResponseDto> CreateAsync(PersonRequestDto dto)
    {
        logger.LogInformation("Criando pessoa: {Name}", dto.Name);

        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors;
            throw new ValidationException(errors);
        }

        var person = adapter.ToPerson(dto);

        await personRepository.CreatePersonAsync(person);

        logger.LogInformation("Pessoa criada com sucesso: {Id}", person.Id);
        var result = adapter.ToPersonResponseDto(person);

        return result;
    }
    /// <summary>
    /// Busca todas as pessoas.
    /// </summary>
    public async Task<List<PersonResponseDto>> GetAllAsync()
    {
        var persons = await personRepository.GetAllPersonsAsync();
        var result = persons.Select(adapter.ToPersonResponseDto).ToList();

        return result;
    }
    /// <summary>
    /// Busca uma pessoa pelo Id.
    /// </summary>
    public async Task<PersonResponseDto> GetByIdAsync(Guid id)
    {
        var person = await personRepository.GetPersonByIdAsync(id);

        if (person is null)
        {
            throw new NotFoundException(Resource.PersonNotFoundCode, Resource.PersonNotFound);
        }
        var result = adapter.ToPersonResponseDto(person);

        return result;
    }

    /// <summary>
    /// Deleta a pessoa e retorna o Id.
    /// </summary>
    public async Task<Guid> DeleteAsync(Guid id)
    {
        var person = await personRepository.GetPersonByIdAsync(id);

        if (person is null)
        {
            throw new NotFoundException(Resource.PersonNotFoundCode, Resource.PersonNotFoundCode);
        }

        await personRepository.DeletePersonAsync(person);

        logger.LogInformation("Pessoa deletada com sucesso: {Id}", id);

        return person.Id;
    }
}