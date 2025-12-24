using ControleGastosResidenciais.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastosResidenciais.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public CategoryPurpose Purpose { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
