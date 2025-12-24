using ControleGastosResidenciais.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastosResidenciais.Domain.Entities;

[Table("categories")]
public class Category
{
    [Key]
    [Required]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("description")]
    public required string Description { get; set; }
    
    [Required]
    [Column("purpose")]
    public CategoryPurpose Purpose { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
