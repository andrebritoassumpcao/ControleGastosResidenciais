using ControleGastosResidenciais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidenciais.Infrastructure.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Age)
            .HasColumnName("age")
            .IsRequired();

        builder.HasIndex(p => p.Name)
            .IsUnique();

        builder.HasMany(p => p.Transactions)
            .WithOne(t => t.Person)
            .HasForeignKey(t => t.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
