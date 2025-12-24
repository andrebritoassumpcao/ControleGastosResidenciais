using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastosResidenciais.Domain.Entities;

public class Person
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}