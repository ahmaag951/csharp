namespace RelationshipsAndCascadeDelete.Entities;

// ONE-TO-MANY: one Author has many Books
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    // Foreign key pointing to Author
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    // Many-to-many with Tag via explicit join entity BookTag
    public ICollection<BookTag> BookTags { get; set; } = new List<BookTag>();
}

// MANY-TO-MANY (explicit join entity): Book <-> Tag
public class Tag
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public ICollection<BookTag> BookTags { get; set; } = new List<BookTag>();
}

// Explicit join entity — lets you add extra columns to the join table
// you don't have to create a separate entity for the many to many relationship, if you put the navigation properties in the two main entites the ef will make the relationship automatically
public class BookTag
{
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;

    public DateTime TaggedOn { get; set; } = DateTime.UtcNow; // extra column
}

// ONE-TO-ONE: one User has one UserProfile
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public UserProfile? Profile { get; set; } // optional one-to-one
}

public class UserProfile
{
    public int Id { get; set; }
    public string Bio { get; set; } = string.Empty;

    // Foreign key for the one-to-one relationship
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
