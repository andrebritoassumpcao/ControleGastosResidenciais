using ControleGastosResidenciais.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastosResidenciais.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public decimal Value { get; set; }
    public TransactionType Type { get; set; }

    public Guid PersonId { get; set; }
    public Person Person { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
