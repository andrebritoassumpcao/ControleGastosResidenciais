using ControleGastosResidenciais.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastosResidenciais.Domain.Entities
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [Required]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("description")]
        public required string Description { get; set; }

        [Required]
        [Column("value")]
        public decimal Value { get; set; }

        [Required]
        [Column("type")]
        public TransactionType Type { get; set; }

        [Required]
        [ForeignKey(nameof(Person))]
        [Column("person_id")]
        public Guid PersonId { get; set; }

        public Person Person { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Category))]
        [Column("categoryId")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;

    }
}
