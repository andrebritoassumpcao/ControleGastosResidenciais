using ControleGastosResidenciais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidenciais.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Value)
            .HasColumnName("value")
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.Type)
            .HasColumnName("type")
            .IsRequired();

        builder.Property(t => t.PersonId)
            .HasColumnName("person_id")
            .IsRequired();

        builder.Property(t => t.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.HasOne(t => t.Person)
            .WithMany(p => p.Transactions)
            .HasForeignKey(t => t.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
