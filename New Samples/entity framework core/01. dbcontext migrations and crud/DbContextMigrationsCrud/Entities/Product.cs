namespace DbContextMigrationsCrud.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    // Foreign key
    public int CategoryId { get; set; }

    // Navigation property: one Product belongs to one Category
    public Category Category { get; set; } = null!;
}
