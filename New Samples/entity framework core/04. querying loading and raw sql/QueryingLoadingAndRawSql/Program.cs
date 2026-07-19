using Microsoft.EntityFrameworkCore;
using QueryingLoadingAndRawSql;
using QueryingLoadingAndRawSql.Entities;

// ============================================================
// SAMPLE 04: Querying, Loading Strategies & Raw SQL
// ============================================================
//
// BEFORE RUNNING:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
// ============================================================

using var db = new AppDbContext();

// ── SEED DATA ────────────────────────────────────────────────
var blog = new Blog { Title = "Tech Blog", Author = "Alice" };
var post1 = new Post { Content = "Intro to EF Core", Views = 150, Blog = blog };
var post2 = new Post { Content = "Advanced LINQ", Views = 320, Blog = blog };
var post3 = new Post { Content = "Raw SQL in EF", Views = 80, Blog = blog };
post1.Comments.Add(new Comment { Text = "Great post!" });
post1.Comments.Add(new Comment { Text = "Very helpful" });
post2.Comments.Add(new Comment { Text = "Loved it" });
db.Blogs.Add(blog);
db.SaveChanges();
Console.WriteLine("Data seeded.\n");

// ── LINQ QUERIES ─────────────────────────────────────────────
Console.WriteLine("=== LINQ QUERIES ===");

// Where + OrderBy
var popularPosts = db.Posts
    .Where(p => p.Views > 100)
    .OrderByDescending(p => p.Views)
    .ToList();
Console.WriteLine($"Popular posts (>100 views): {popularPosts.Count}");
foreach (var p in popularPosts)
    Console.WriteLine($"  '{p.Content}' — {p.Views} views");

// GroupBy
var grouped = db.Posts
    .GroupBy(p => p.BlogId)
    .Select(g => new { BlogId = g.Key, TotalViews = g.Sum(p => p.Views) })
    .ToList();
Console.WriteLine($"\nTotal views per blog:");
foreach (var g in grouped)
    Console.WriteLine($"  Blog #{g.BlogId}: {g.TotalViews} total views");

// ── EAGER LOADING ─────────────────────────────────────────────
Console.WriteLine("\n=== EAGER LOADING (Include / ThenInclude) ===");

// Include() + ThenInclude() — loads the whole graph in one SQL query (JOIN)
var blogsEager = db.Blogs
    .Include(b => b.Posts)
        .ThenInclude(p => p.Comments)
    .ToList();

foreach (var b in blogsEager)
{
    Console.WriteLine($"Blog: {b.Title}");
    foreach (var p in b.Posts)
    {
        Console.WriteLine($"  Post: {p.Content} ({p.Comments.Count} comments)");
    }
}

// ── EXPLICIT LOADING ──────────────────────────────────────────
Console.WriteLine("\n=== EXPLICIT LOADING ===");

// Start with just the post — no navigation loaded yet
var singlePost = db.Posts.First(p => p.Id == post2.Id);
Console.WriteLine($"Post loaded: '{singlePost.Content}' — comments loaded? {db.Entry(singlePost).Collection(p => p.Comments).IsLoaded}");

// Explicitly load the Comments for this specific post
db.Entry(singlePost).Collection(p => p.Comments).Load();
Console.WriteLine($"After explicit load — comments: {singlePost.Comments.Count}");

// Also explicitly load the Blog reference navigation
db.Entry(singlePost).Reference(p => p.Blog).Load();
Console.WriteLine($"Blog of this post: {singlePost.Blog.Title}");

// ── LAZY LOADING ──────────────────────────────────────────────
Console.WriteLine("\n=== LAZY LOADING ===");
// Lazy loading fires an extra DB query the first time you access a navigation property
// Requires: virtual navigation properties + UseLazyLoadingProxies() in OnConfiguring

using var db2 = new AppDbContext();
var lazyPost = db2.Posts.First(); // no Include here
// Accessing .Blog triggers a lazy-load query automatically
Console.WriteLine($"Lazy-loaded blog title: {lazyPost.Blog.Title}");
// Accessing .Comments triggers another lazy-load query
Console.WriteLine($"Lazy-loaded comment count: {lazyPost.Comments.Count}");

// ── PROJECTION (DTO) ──────────────────────────────────────────
Console.WriteLine("\n=== PROJECTION TO DTO ===");

var summaries = db.Posts
    .Select(p => new
    {
        p.Content,
        p.Views,
        CommentCount = p.Comments.Count
    })
    .ToList();

foreach (var s in summaries)
    Console.WriteLine($"  '{s.Content}' | Views: {s.Views} | Comments: {s.CommentCount}");

// ── RAW SQL ───────────────────────────────────────────────────
Console.WriteLine("\n=== RAW SQL ===");

// FromSqlRaw — query with raw SQL but still returns tracked entities
var rawPosts = db.Posts
    .FromSqlRaw("SELECT * FROM Posts WHERE Views > {0}", 100)
    .ToList();
Console.WriteLine($"Raw SQL found {rawPosts.Count} posts with >100 views");

// ExecuteSqlRaw — for non-query statements (UPDATE, DELETE, etc.)
int affected = db.Database.ExecuteSqlRaw("UPDATE Posts SET Views = Views + 1 WHERE BlogId = {0}", blog.Id);
Console.WriteLine($"ExecuteSqlRaw updated {affected} rows (incremented views)");

Console.WriteLine("\nDone!");
