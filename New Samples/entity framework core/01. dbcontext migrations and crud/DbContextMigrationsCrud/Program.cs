using DbContextMigrationsCrud;
using DbContextMigrationsCrud.Entities;

// ============================================================
// SAMPLE 01: DbContext, Entities, Migrations & CRUD
// ============================================================
//
// BEFORE RUNNING — apply the migration to create the database:
//   1. Open a terminal in this project folder
//   2. Run: dotnet ef migrations add InitialCreate
//   3. Run: dotnet ef database update
//
// Or with Package Manager Console:
//   Add-Migration InitialCreate
//   Update-Database
// ============================================================

using var db = new AppDbContext();

// ── CREATE ──────────────────────────────────────────────────
Console.WriteLine("=== CREATE ===");

var electronics = new Category { Name = "Electronics" };
var food = new Category { Name = "Food" };

// Add() marks the entity as Added — it will be INSERTed on SaveChanges
db.Categories.Add(electronics);
db.Categories.Add(food);
db.SaveChanges(); // Executes INSERT for both categories
Console.WriteLine($"Added categories: {electronics.Name} (Id={electronics.Id}), {food.Name} (Id={food.Id})");

var laptop = new Product { Name = "Laptop", Price = 999.99m, CategoryId = electronics.Id };
var phone = new Product { Name = "Phone", Price = 499.99m, CategoryId = electronics.Id };
var apple = new Product { Name = "Apple", Price = 0.99m, CategoryId = food.Id };

db.Products.AddRange(laptop, phone, apple); // AddRange adds multiple at once
db.SaveChanges();
Console.WriteLine($"Added products: {laptop.Name} (Id={laptop.Id}), {phone.Name}, {apple.Name}");

// ── READ ─────────────────────────────────────────────────────
Console.WriteLine("\n=== READ ===");

// Find() looks up by primary key — uses the cache first, then hits the DB
var foundProduct = db.Products.Find(laptop.Id);
Console.WriteLine($"Found by PK: {foundProduct?.Name}");

var foundProductByName = db.Products.Where(p => p.Name == laptop.Name);
Console.WriteLine($"Found by Name: {foundProduct?.Id}");


// ToList() executes a SELECT query and materializes all rows
// .ToList() is the trigger that sends the SQL to the database and loads the results into actual C# objects in memory.
// That loading step is called materializing — turning database rows into real objects.
var allProducts = db.Products.ToList();
Console.WriteLine($"All products ({allProducts.Count}):");
foreach (var p in allProducts)
    Console.WriteLine($"  - {p.Name}: ${p.Price}");

// FirstOrDefault returns the first match or null
var cheap = db.Products.FirstOrDefault(p => p.Price < 1m);
Console.WriteLine($"Cheapest under $1: {cheap?.Name ?? "none"}");

// ── UPDATE ───────────────────────────────────────────────────
Console.WriteLine("\n=== UPDATE ===");

// EF tracks changes automatically — just modify and call SaveChanges
laptop.Price = 899.99m;
db.SaveChanges(); // Executes UPDATE only for the changed columns
Console.WriteLine($"Updated Laptop price to: ${laptop.Price}");

// ── DELETE ───────────────────────────────────────────────────
Console.WriteLine("\n=== DELETE ===");

// Remove() marks the entity as Deleted — it will be DELETEd on SaveChanges
db.Products.Remove(apple);
db.SaveChanges();
Console.WriteLine($"Deleted: {apple.Name}");

var remaining = db.Products.ToList();
Console.WriteLine($"Remaining products: {string.Join(", ", remaining.Select(p => p.Name))}");

Console.WriteLine("\nDone!");
