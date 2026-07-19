using Microsoft.EntityFrameworkCore;
using TransactionsConcurrencyAndResiliency.Entities;

namespace TransactionsConcurrencyAndResiliency;

public class AppDbContext : DbContext
{
    public DbSet<BankAccount> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=.;Database=testEntityFramework2;Trusted_Connection=True;TrustServerCertificate=True;",
            sqlOptions =>
            {
                // Connection Resiliency: automatically retry transient failures (deadlocks,
                // timeouts, network blips) up to 5 times with exponential back-off
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });
    }
}
