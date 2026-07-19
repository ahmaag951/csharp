using DataAnnotationsAndFluentApi;
using DataAnnotationsAndFluentApi.Entities;
using Microsoft.EntityFrameworkCore;

// ============================================================
// SAMPLE 03: Data Annotations & Fluent API
// ============================================================
//
// BEFORE RUNNING:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
// ============================================================

using var db = new AppDbContext();

// ── DATA ANNOTATIONS in action ───────────────────────────────
Console.WriteLine("=== DATA ANNOTATIONS ===");

var hr = new Department { Name = "Human Resources" };
db.Departments.Add(hr);
db.SaveChanges();

var employee = new Employee
{
    FirstName = "Jane",
    LastName = "Doe",
    Email = "jane.doe@example.com",
    HireDate = new DateTime(2023, 3, 15),
    Status = EmploymentStatus.Active,      // stored as "Active" string in DB (Value Conversion)
    DepartmentId = hr.Id,
    HomeAddress = new Address             // Owned Type — no separate table
    {
        Street = "123 Main St",
        City = "New York",
        Country = "USA"
    }
};
db.Employees.Add(employee);
db.SaveChanges();
Console.WriteLine($"Saved: {employee.FirstName} {employee.LastName}");

// ── FLUENT API — read back and show ──────────────────────────
Console.WriteLine("\n=== READ BACK ===");

var loaded = db.Employees
    .Include(e => e.Department)
    .First(e => e.Id == employee.Id);

Console.WriteLine($"Employee    : {loaded.FirstName} {loaded.LastName}");
Console.WriteLine($"Email       : {loaded.Email}");
Console.WriteLine($"Hire Date   : {loaded.HireDate:d}");
Console.WriteLine($"Status      : {loaded.Status}");        // enum displayed as "Active"
Console.WriteLine($"Department  : {loaded.Department.Name}");
Console.WriteLine($"Address     : {loaded.HomeAddress.Street}, {loaded.HomeAddress.City}, {loaded.HomeAddress.Country}");

// ── VALUE CONVERSION — see raw DB value ──────────────────────
Console.WriteLine("\n=== VALUE CONVERSION (enum as string) ===");
// In the DB the Status column holds "Active" (not 0)
var raw = db.Employees
    .Select(e => new { e.FirstName, StatusRaw = e.Status.ToString() })
    .First();
Console.WriteLine($"DB stores Status as: '{raw.StatusRaw}'");

// ── UPDATE STATUS ─────────────────────────────────────────────
loaded.Status = EmploymentStatus.OnLeave;
db.SaveChanges();
Console.WriteLine($"\nUpdated status to: {loaded.Status}");

Console.WriteLine("\nDone!");
