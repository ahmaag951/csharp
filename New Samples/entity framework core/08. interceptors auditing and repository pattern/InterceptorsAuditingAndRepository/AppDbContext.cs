using InterceptorsAuditingAndRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InterceptorsAuditingAndRepository;

// ── AUDITING INTERCEPTOR ──────────────────────────────────────
// ISaveChangesInterceptor lets you hook into the save pipeline.
// Here we auto-populate CreatedAt and UpdatedAt before every save.
public class AuditInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        SetAuditFields(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        SetAuditFields(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void SetAuditFields(DbContext? context)
    {
        if (context is null) return;

        var now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
                // Prevent overwriting the original CreatedAt
                entry.Property(e => e.CreatedAt).IsModified = false;
            }
        }
    }
}

// ── DBCONTEXT ─────────────────────────────────────────────────
public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseSqlServer(
                "Server=.;Database=testEntityFramework2;Trusted_Connection=True;TrustServerCertificate=True;")
            .AddInterceptors(new AuditInterceptor()); // register the interceptor
    }
}
