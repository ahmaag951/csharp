using Microsoft.EntityFrameworkCore;
using TransactionsConcurrencyAndResiliency;
using TransactionsConcurrencyAndResiliency.Entities;

// ============================================================
// SAMPLE 06: Transactions, Concurrency & Resiliency
// ============================================================
//
// BEFORE RUNNING:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
// ============================================================

using var db = new AppDbContext();

// ── SEED ACCOUNTS ─────────────────────────────────────────────
var alice = new BankAccount { Owner = "Alice", Balance = 1000m };
var bob = new BankAccount { Owner = "Bob", Balance = 500m };
db.Accounts.AddRange(alice, bob);
db.SaveChanges();
Console.WriteLine($"Accounts: Alice=${alice.Balance}, Bob=${bob.Balance}");

// ── TRANSACTIONS ──────────────────────────────────────────────
Console.WriteLine("\n=== EXPLICIT TRANSACTION ===");

// A transaction guarantees that all operations succeed together or all fail together
// Without a transaction, a crash between the two SaveChanges would leave data inconsistent
using (var txn = db.Database.BeginTransaction())
{
    try
    {
        decimal transferAmount = 200m;

        alice.Balance -= transferAmount;
        db.SaveChanges(); // step 1

        bob.Balance += transferAmount;
        db.SaveChanges(); // step 2

        var log = new Transaction
        {
            FromAccountId = alice.Id,
            ToAccountId = bob.Id,
            Amount = transferAmount
        };
        db.Transactions.Add(log);
        db.SaveChanges(); // step 3

        txn.Commit(); // Only now are all changes permanent in the DB
        Console.WriteLine($"Transfer complete. Alice=${alice.Balance}, Bob=${bob.Balance}");
    }
    catch (Exception ex)
    {
        txn.Rollback(); // Undo all three steps if anything went wrong
        Console.WriteLine($"Transaction rolled back: {ex.Message}");
    }
}

// ── TRANSACTION ROLLBACK DEMO ─────────────────────────────────
Console.WriteLine("\n=== TRANSACTION ROLLBACK DEMO ===");

using (var txn = db.Database.BeginTransaction())
{
    try
    {
        alice.Balance -= 9999m; // intentionally overspend
        db.SaveChanges();

        if (alice.Balance < 0)
            throw new InvalidOperationException("Insufficient funds!");

        txn.Commit();
    }
    catch (Exception ex)
    {
        txn.Rollback();
        // Reload from DB to get the correct value back
        db.Entry(alice).Reload();
        Console.WriteLine($"Rolled back. Alice balance unchanged: ${alice.Balance}");
        Console.WriteLine($"Reason: {ex.Message}");
    }
}

// ── CONCURRENCY TOKEN ─────────────────────────────────────────
Console.WriteLine("\n=== CONCURRENCY TOKEN ([Timestamp]) ===");

// Simulate two users reading the same row
using var db1 = new AppDbContext();
using var db2 = new AppDbContext();

var aliceInSession1 = db1.Accounts.First(a => a.Owner == "Alice");
var aliceInSession2 = db2.Accounts.First(a => a.Owner == "Alice");

// Session 1 saves first
aliceInSession1.Balance += 50m;
db1.SaveChanges(); // succeeds — RowVersion updated in DB
Console.WriteLine($"Session 1 saved Alice balance: ${aliceInSession1.Balance}");

// Session 2 tries to save with a stale RowVersion
aliceInSession2.Balance += 100m;
try
{
    db2.SaveChanges(); // throws because RowVersion no longer matches
}
catch (DbUpdateConcurrencyException ex)
{
    // Reload the current DB values
    var entry = ex.Entries.Single();
    var dbValues = entry.GetDatabaseValues()!;
    var dbBalance = (decimal)dbValues["Balance"]!;

    Console.WriteLine($"Concurrency conflict detected!");
    Console.WriteLine($"  Session 2 expected balance: ${aliceInSession2.Balance - 100m}");
    Console.WriteLine($"  DB currently has         : ${dbBalance}");
    Console.WriteLine($"  Session 2 wanted to set  : ${aliceInSession2.Balance}");
    Console.WriteLine("  Resolution: reload and retry (client wins / server wins / merge)");
}

Console.WriteLine("\nDone!");
