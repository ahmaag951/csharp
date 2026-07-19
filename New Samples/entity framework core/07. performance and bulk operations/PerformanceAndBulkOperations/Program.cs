using Microsoft.EntityFrameworkCore;
using PerformanceAndBulkOperations;
using PerformanceAndBulkOperations.Entities;
using System.Diagnostics;

// ============================================================
// SAMPLE 07: Performance & Bulk Operations
// ============================================================
//
// BEFORE RUNNING:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
// ============================================================

using var db = new AppDbContext();

// ── SEED ─────────────────────────────────────────────────────
Console.WriteLine("Seeding 500 orders...");
var orders = Enumerable.Range(1, 500).Select(i => new Order
{
    CustomerName = $"Customer {i}",
    Total = (i % 10 + 1) * 25m,
    Status = i % 3 == 0 ? "Shipped" : "Pending"
}).ToList();
db.Orders.AddRange(orders);
db.SaveChanges();
Console.WriteLine($"Seeded {orders.Count} orders.\n");

// ── AsNoTracking ──────────────────────────────────────────────
Console.WriteLine("=== AsNoTracking ===");
// By default, EF tracks every loaded entity in memory (change tracking).
// AsNoTracking() skips tracking — faster for read-only queries because:
//   - No snapshot is stored for change detection
//   - No identity resolution overhead

var sw = Stopwatch.StartNew();
var trackedOrders = db.Orders.Where(o => o.Status == "Pending").ToList();
sw.Stop();
Console.WriteLine($"With tracking    : {trackedOrders.Count} orders in {sw.ElapsedMilliseconds}ms");

sw.Restart();
var untrackedOrders = db.Orders.AsNoTracking().Where(o => o.Status == "Pending").ToList();
sw.Stop();
Console.WriteLine($"AsNoTracking     : {untrackedOrders.Count} orders in {sw.ElapsedMilliseconds}ms");

// ── COMPILED QUERIES ──────────────────────────────────────────
Console.WriteLine("\n=== COMPILED QUERIES ===");
// EF compiles a LINQ query to SQL on first call, then caches it.
// EF.CompileQuery pre-compiles it at startup — eliminates per-call compilation overhead.
// Best for hot paths called thousands of times.

var getOrdersByStatus = EF.CompileQuery(
    (AppDbContext ctx, string status) => ctx.Orders.Where(o => o.Status == status));

sw.Restart();
var compiled = getOrdersByStatus(db, "Pending").ToList();
sw.Stop();
Console.WriteLine($"Compiled query   : {compiled.Count} pending orders in {sw.ElapsedMilliseconds}ms");

// ── BULK UPDATE (EF Core 7+) ──────────────────────────────────
Console.WriteLine("\n=== BULK UPDATE (ExecuteUpdateAsync) ===");
// Traditional approach: load all rows → modify → SaveChanges (N round trips)
// ExecuteUpdateAsync: single UPDATE SQL statement — much faster for large sets

int updatedCount = await db.Orders
    .Where(o => o.Status == "Pending" && o.Total > 100m)
    .ExecuteUpdateAsync(setters => setters
        .SetProperty(o => o.Status, "Processing"));

Console.WriteLine($"Bulk updated {updatedCount} orders to 'Processing' in one SQL statement");

// ── BULK DELETE (EF Core 7+) ──────────────────────────────────
Console.WriteLine("\n=== BULK DELETE (ExecuteDeleteAsync) ===");
// Single DELETE SQL statement — does NOT load entities into memory first

int deletedCount = await db.Orders
    .Where(o => o.Status == "Shipped")
    .ExecuteDeleteAsync();

Console.WriteLine($"Bulk deleted {deletedCount} shipped orders in one SQL statement");

int remaining = db.Orders.Count();
Console.WriteLine($"\nRemaining orders in DB: {remaining}");

Console.WriteLine("\nDone!");
