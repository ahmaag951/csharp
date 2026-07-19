using Microsoft.EntityFrameworkCore;
using SeedingGlobalFiltersAndSoftDelete.Entities;

namespace SeedingGlobalFiltersAndSoftDelete;

public class AppDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=.;Database=testEntityFramework2;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ── GLOBAL QUERY FILTER ────────────────────────────────
        // Applied automatically to every query on Article — hides soft-deleted rows
        modelBuilder.Entity<Article>()
            .HasQueryFilter(a => !a.IsDeleted);

        modelBuilder.Entity<Tag>()
            .HasQueryFilter(t => !t.IsDeleted);

        // ── SEED DATA (HasData) ────────────────────────────────
        // HasData inserts rows when you run migrations — good for reference/lookup data
        // IMPORTANT: IDs must be hardcoded (not auto-generated) for seeding to work
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Technology" },
            new Tag { Id = 2, Name = "Science" },
            new Tag { Id = 3, Name = "Health" }
        );

        modelBuilder.Entity<Article>().HasData(
            new Article { Id = 1, Title = "AI in 2025", Body = "Artificial intelligence is evolving rapidly..." },
            new Article { Id = 2, Title = "Quantum Computing", Body = "Quantum computers promise exponential speedups..." },
            new Article { Id = 3, Title = "Sleep Science", Body = "Quality sleep is essential for cognitive function..." }
        );
    }
}
