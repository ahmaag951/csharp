using DataAnnotationsAndFluentApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAnnotationsAndFluentApi;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=.;Database=testEntityFramework2;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ── Fluent API for Department ─────────────────────────
        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Departments");

            entity.HasKey(d => d.Id);

            entity.Property(d => d.Name)
                  .IsRequired()
                  .HasMaxLength(150);

            // One Department has many Employees
            entity.HasMany(d => d.Employees)
                  .WithOne(e => e.Department)
                  .HasForeignKey(e => e.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ── Fluent API for Employee ───────────────────────────
        modelBuilder.Entity<Employee>(entity =>
        {
            // Value Conversion: store EmploymentStatus enum as a string in the DB
            // Without this, EF stores enums as integers by default
            entity.Property(e => e.Status)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            // Owned Type: Address columns are embedded inside the Employees table
            // The columns will be named HomeAddress_Street, HomeAddress_City, HomeAddress_Country
            entity.OwnsOne(e => e.HomeAddress, address =>
            {
                address.Property(a => a.Street).HasMaxLength(200);
                address.Property(a => a.City).HasMaxLength(100);
                address.Property(a => a.Country).HasMaxLength(100);
            });
        });
    }
}
