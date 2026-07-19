using Microsoft.EntityFrameworkCore;
using QueryingLoadingAndRawSql.Entities;

namespace QueryingLoadingAndRawSql;

public class AppDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseSqlServer(
                "Server=.;Database=testEntityFramework2;Trusted_Connection=True;TrustServerCertificate=True;")
            // Enable Lazy Loading — requires virtual navigation properties and the Proxies package
            .UseLazyLoadingProxies();
    }
}
