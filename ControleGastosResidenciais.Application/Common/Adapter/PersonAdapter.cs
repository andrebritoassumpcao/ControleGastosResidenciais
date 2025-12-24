using ControleGastosResidenciais.Application.Common.Adapter.Interface;
using ControleGastosResidenciais.Application.DTOs.Persons;
using ControleGastosResidenciais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Common.Adapter;

public class PersonAdapter : IPersonAdapter
{
    public PersonResponseDto ToPersonResponseDto(Person person)
    => new()
    {
        Id = person.Id,
        Name = person.Name,
        Age = person.Age
    };

    public Person ToPerson(PersonRequestDto personDto)
    => new()
    {
        Id = Guid.NewGuid(),
        Name = personDto.Name,
        Age = personDto.Age
    };
}
