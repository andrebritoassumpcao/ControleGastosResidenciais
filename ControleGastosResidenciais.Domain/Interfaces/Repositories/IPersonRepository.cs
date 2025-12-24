using ControleGastosResidenciais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Domain.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<Person?> GetPersonByIdAsync(Guid id);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task CreatePersonAsync(Person person);
        Task<Guid> DeletePersonAsync(Person person);
        Task<bool> NameAlreadyExistis(string name);

    }
}
