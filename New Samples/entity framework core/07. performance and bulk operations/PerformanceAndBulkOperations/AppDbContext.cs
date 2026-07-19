using Microsoft.EntityFrameworkCore;
using PerformanceAndBulkOperations.Entities;

namespace PerformanceAndBulkOperations;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=.;Database=testEntityFramework2;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
