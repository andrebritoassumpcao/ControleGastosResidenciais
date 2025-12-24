using ControleGastosResidenciais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidenciais.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
