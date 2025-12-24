using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastosResidenciais.Domain.Entities;

[Table("persons")]
public class Person
{
    [Key]
    [Required]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("name")]
    public required string Name { get; set; }

    [Required]
    [Column("age")]
    public int Age { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}