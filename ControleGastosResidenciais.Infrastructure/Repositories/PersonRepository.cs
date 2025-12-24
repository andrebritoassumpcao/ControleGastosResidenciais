using ControleGastosResidenciais.Domain.Entities;
using ControleGastosResidenciais.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidenciais.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Person usando Entity Framework.
/// </summary>
public class PersonRepository(AppDbContext context) : IPersonRepository
{
    public async Task<Person?> GetPersonByIdAsync(Guid id)
    {
        var result = await context.Persons
            .FirstOrDefaultAsync(p => p.Id == id);

        return result;
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        var result = await context.Persons
            .OrderBy(p => p.Name)
            .ToListAsync();

        return result;
    }

    public async Task CreatePersonAsync(Person person)
    {
        await context.Persons.AddAsync(person);

        await context.SaveChangesAsync();
    }

    public async Task<Guid> DeletePersonAsync(Person person)
    {
        context.Persons.Remove(person);
        await context.SaveChangesAsync();

        return person.Id;
    }

    public async Task<bool> NameAlreadyExistis(string name)
    {
        var result = await context.Persons
            .FirstOrDefaultAsync(p => p.Name == name);

        if(result is not null)
        {
            return true;
        }

        return false;
    }
}
