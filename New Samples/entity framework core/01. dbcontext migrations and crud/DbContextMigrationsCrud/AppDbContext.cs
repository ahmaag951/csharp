using DbContextMigrationsCrud.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbContextMigrationsCrud;

// DbContext is the bridge between your C# classes and the database.
// Each DbSet<T> maps to a table.
public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=.;Database=testEntityFramework2;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
