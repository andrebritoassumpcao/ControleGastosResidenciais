using ControleGastosResidenciais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Categories
{
    public class CategoryResponseDto
    {
        public Guid Id { get; init; }

        public string Description { get; init; } = string.Empty;

        public CategoryPurpose Purpose { get; init; }
    }
}
