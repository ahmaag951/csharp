using Microsoft.EntityFrameworkCore;
using SeedingGlobalFiltersAndSoftDelete;
using SeedingGlobalFiltersAndSoftDelete.Entities;

// ============================================================
// SAMPLE 05: Seeding, Global Filters & Soft Delete
// ============================================================
//
// BEFORE RUNNING:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
//   (The migration will also INSERT the seed data via HasData)
// ============================================================

using var db = new AppDbContext();

// ── SEED DATA ────────────────────────────────────────────────
Console.WriteLine("=== SEED DATA (loaded by migration) ===");

// The Tags and Articles were inserted by HasData in the migration
var tags = db.Tags.ToList();
Console.WriteLine($"Seeded tags ({tags.Count}):");
foreach (var t in tags)
    Console.WriteLine($"  - {t.Name}");

var articles = db.Articles.ToList();
Console.WriteLine($"\nSeeded articles ({articles.Count}):");
foreach (var a in articles)
    Console.WriteLine($"  - {a.Title}");

// ── GLOBAL FILTER + SOFT DELETE ───────────────────────────────
Console.WriteLine("\n=== SOFT DELETE + GLOBAL FILTER ===");

// Instead of DELETE, we mark the record as deleted
var articleToDelete = db.Articles.First(a => a.Id == 1);
articleToDelete.IsDeleted = true;
articleToDelete.DeletedAt = DateTime.UtcNow;
db.SaveChanges();
Console.WriteLine($"Soft-deleted: '{articleToDelete.Title}'");

// Global query filter hides deleted rows automatically — no WHERE clause needed
var visibleArticles = db.Articles.ToList();
Console.WriteLine($"\nArticles visible (with global filter): {visibleArticles.Count}");
foreach (var a in visibleArticles)
    Console.WriteLine($"  - {a.Title}");

// ── IGNORE QUERY FILTERS ──────────────────────────────────────
Console.WriteLine("\n=== IGNORE QUERY FILTERS ===");

// IgnoreQueryFilters() bypasses the global filter — useful for admin views
var allArticles = db.Articles.IgnoreQueryFilters().ToList();
Console.WriteLine($"All articles including soft-deleted: {allArticles.Count}");
foreach (var a in allArticles)
    Console.WriteLine($"  - {a.Title} {(a.IsDeleted ? "(DELETED)" : "")}");

// ── RESTORE (un-delete) ───────────────────────────────────────
Console.WriteLine("\n=== RESTORE ===");

var deleted = db.Articles.IgnoreQueryFilters().First(a => a.IsDeleted);
deleted.IsDeleted = false;
deleted.DeletedAt = null;
db.SaveChanges();
Console.WriteLine($"Restored: '{deleted.Title}'");
Console.WriteLine($"Visible articles now: {db.Articles.Count()}");

Console.WriteLine("\nDone!");
