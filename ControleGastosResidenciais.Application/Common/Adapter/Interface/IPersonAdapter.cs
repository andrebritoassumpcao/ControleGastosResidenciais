using ControleGastosResidenciais.Application.DTOs.Persons;
using ControleGastosResidenciais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.Common.Adapter.Interface;

public interface IPersonAdapter
{
    PersonResponseDto ToPersonResponseDto(Person person);
    Person ToPerson(PersonRequestDto personDto);
}
