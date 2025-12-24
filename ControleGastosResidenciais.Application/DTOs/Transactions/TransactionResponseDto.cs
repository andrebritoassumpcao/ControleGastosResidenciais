using ControleGastosResidenciais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Application.DTOs.Transactions
{
    public class TransactionResponseDto
    {
        public Guid Id { get; init; }

        public string Description { get; init; } = string.Empty;

        public decimal Value { get; init; }

        public TransactionType Type { get; init; }

        public Guid PersonId { get; init; }

        public string PersonName { get; init; } = string.Empty;

        public Guid CategoryId { get; init; }
    }
}
