using Microsoft.EntityFrameworkCore;
using SplitItUp.Domain;

namespace SplitItUp.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {}

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=splititup;User Id=postgres;Password=admin");
    // }
    
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<Spending> Spendings { get; set; }
    public virtual DbSet<SpendingShare> SpendingShares { get; set; }
}