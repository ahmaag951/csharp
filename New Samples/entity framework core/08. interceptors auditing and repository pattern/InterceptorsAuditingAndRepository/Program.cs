using InterceptorsAuditingAndRepository;
using InterceptorsAuditingAndRepository.Entities;
using Microsoft.EntityFrameworkCore;

// ============================================================
// SAMPLE 08: Interceptors, Auditing & Repository Pattern
// ============================================================
//
// BEFORE RUNNING:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
// ============================================================

// ── REPOSITORY PATTERN ───────────────────────────────────────
Console.WriteLine("=== REPOSITORY PATTERN ===");

var db = new AppDbContext();
var customerRepo = new Repository<Customer>(db);
var invoiceRepo = new Repository<Invoice>(db);

// Add via repository — business logic never touches DbContext directly
var customer = new Customer { Name = "Acme Corp", Email = "acme@example.com" };
await customerRepo.AddAsync(customer);
await customerRepo.SaveAsync();
Console.WriteLine($"Added customer: {customer.Name} (Id={customer.Id})");

var inv1 = new Invoice { Amount = 500m, IsPaid = false, CustomerId = customer.Id };
var inv2 = new Invoice { Amount = 1200m, IsPaid = true, CustomerId = customer.Id };
await invoiceRepo.AddAsync(inv1);
await invoiceRepo.AddAsync(inv2);
await invoiceRepo.SaveAsync();
Console.WriteLine($"Added {2} invoices");

// Find via predicate
var unpaid = await invoiceRepo.FindAsync(i => !i.IsPaid);
Console.WriteLine($"Unpaid invoices: {unpaid.Count}");

// ── AUDITING INTERCEPTOR ──────────────────────────────────────
Console.WriteLine("\n=== AUDITING INTERCEPTOR (auto CreatedAt / UpdatedAt) ===");

// CreatedAt and UpdatedAt were set automatically by the AuditInterceptor
var saved = await customerRepo.GetByIdAsync(customer.Id);
Console.WriteLine($"Customer      : {saved!.Name}");
Console.WriteLine($"CreatedAt     : {saved.CreatedAt:u}");
Console.WriteLine($"UpdatedAt     : {saved.UpdatedAt:u}");

// Simulate an update — only UpdatedAt should change
await Task.Delay(100); // small delay so the timestamps differ
saved.Email = "newemail@acme.com";
customerRepo.Update(saved);
await customerRepo.SaveAsync();

var updated = await customerRepo.GetByIdAsync(customer.Id);
Console.WriteLine($"\nAfter update:");
Console.WriteLine($"  Email         : {updated!.Email}");
Console.WriteLine($"  CreatedAt     : {updated.CreatedAt:u}  (unchanged)");
Console.WriteLine($"  UpdatedAt     : {updated.UpdatedAt:u}  (updated)");
Console.WriteLine($"  Same created? : {updated.CreatedAt == saved.CreatedAt}");

// ── INVOICE QUERY VIA REPO ────────────────────────────────────
Console.WriteLine("\n=== QUERY THROUGH REPOSITORY ===");
var allInvoices = await invoiceRepo.GetAllAsync();
foreach (var inv in allInvoices)
    Console.WriteLine($"  Invoice #{inv.Id}: ${inv.Amount} | Paid={inv.IsPaid} | Created={inv.CreatedAt:u}");

// ── REMOVE ENTITY VIA REPO ────────────────────────────────────
Console.WriteLine("\n=== REMOVE ===");
var toRemove = await invoiceRepo.GetByIdAsync(inv2.Id);
invoiceRepo.Remove(toRemove!);
await invoiceRepo.SaveAsync();
Console.WriteLine($"Removed invoice #{inv2.Id}. Remaining: {(await invoiceRepo.GetAllAsync()).Count}");

Console.WriteLine("\nDone!");

db.Dispose();
