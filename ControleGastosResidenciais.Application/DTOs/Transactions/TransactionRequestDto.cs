using ControleGastosResidenciais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Transactions
{
    public class TransactionRequestDto
    {
        public required string Description { get; init; }

        public required decimal Value { get; init; }

        public required TransactionType Type { get; init; }

        public required Guid PersonId { get; init; }

        public required Guid CategoryId { get; init; }
    }
}
