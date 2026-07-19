namespace DbContextMigrationsCrud.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property: one Category has many Products
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
