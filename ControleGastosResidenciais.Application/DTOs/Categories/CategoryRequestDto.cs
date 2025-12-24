using ControleGastosResidenciais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Categories
{
    public class CategoryRequestDto
    {
        public required string Description { get; init; }

        public required CategoryPurpose Purpose { get; init; }
    }
}
