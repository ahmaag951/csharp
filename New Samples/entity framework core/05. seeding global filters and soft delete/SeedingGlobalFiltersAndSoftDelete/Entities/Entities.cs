namespace SeedingGlobalFiltersAndSoftDelete.Entities;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    // Soft delete fields — the row is never physically removed
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}
