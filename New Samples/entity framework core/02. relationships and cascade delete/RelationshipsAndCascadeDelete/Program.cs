using Microsoft.EntityFrameworkCore;
using RelationshipsAndCascadeDelete;
using RelationshipsAndCascadeDelete.Entities;

// ============================================================
// SAMPLE 02: Relationships & Cascade Delete
// ============================================================
//
// BEFORE RUNNING:
//   dotnet ef migrations add InitialCreate
//   dotnet ef database update
// ============================================================

using var db = new AppDbContext();

// ── ONE-TO-MANY ──────────────────────────────────────────────
Console.WriteLine("=== ONE-TO-MANY ===");

var author = new Author { Name = "Robert Martin" };
var book1 = new Book { Title = "Clean Code", Author = author };
var book2 = new Book { Title = "Clean Architecture", Author = author };

// Add the root entity — EF will also insert the related Books
db.Authors.Add(author);
db.SaveChanges();
Console.WriteLine($"Author '{author.Name}' saved with {author.Books.Count} books... Loading from DB:");

// Include() performs a JOIN to load related data (Eager Loading)
var loadedAuthor = db.Authors
    .Include(a => a.Books)
    .First(a => a.Id == author.Id);
Console.WriteLine($"  Author: {loadedAuthor.Name}");
foreach (var b in loadedAuthor.Books)
    Console.WriteLine($"    Book: {b.Title}");

// ── MANY-TO-MANY ─────────────────────────────────────────────
Console.WriteLine("\n=== MANY-TO-MANY ===");

var tagCsharp = new Tag { Label = "C#" };
var tagDesign = new Tag { Label = "Design" };
db.Tags.AddRange(tagCsharp, tagDesign);
db.SaveChanges();

// Create join table rows with an extra column (TaggedOn)
db.BookTags.Add(new BookTag { BookId = book1.Id, TagId = tagCsharp.Id });
db.BookTags.Add(new BookTag { BookId = book1.Id, TagId = tagDesign.Id });
db.BookTags.Add(new BookTag { BookId = book2.Id, TagId = tagDesign.Id });
db.SaveChanges();

// ThenInclude() navigates through the join entity to the Tag
var booksWithTags = db.Books
    .Include(b => b.BookTags)
        .ThenInclude(bt => bt.Tag)
    .ToList();
foreach (var b in booksWithTags)
    Console.WriteLine($"  '{b.Title}' tagged: {string.Join(", ", b.BookTags.Select(bt => bt.Tag.Label))}");

// ── ONE-TO-ONE ───────────────────────────────────────────────
Console.WriteLine("\n=== ONE-TO-ONE ===");

var user = new User { Username = "alice" };
db.Users.Add(user);
db.SaveChanges();

var profile = new UserProfile { UserId = user.Id, Bio = "C# developer" };
db.UserProfiles.Add(profile);
db.SaveChanges();

var userWithProfile = db.Users.Include(u => u.Profile).First(u => u.Id == user.Id);
Console.WriteLine($"  User: {userWithProfile.Username} | Bio: {userWithProfile.Profile?.Bio}");

// ── CASCADE DELETE ───────────────────────────────────────────
Console.WriteLine("\n=== CASCADE DELETE ===");

// Deleting the author will automatically delete their books (DeleteBehavior.Cascade)
Console.WriteLine($"  Books before delete: {db.Books.Count()}");
db.Authors.Remove(loadedAuthor);
db.SaveChanges();
Console.WriteLine($"  Books after deleting author: {db.Books.Count()} (cascaded)");

Console.WriteLine("\nDone!");
