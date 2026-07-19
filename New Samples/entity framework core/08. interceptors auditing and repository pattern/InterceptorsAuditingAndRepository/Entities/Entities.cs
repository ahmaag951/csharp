namespace InterceptorsAuditingAndRepository.Entities;

// Base class that carries audit fields
// All entities that inherit this will get CreatedAt / UpdatedAt automatically
public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Customer : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}

public class Invoice : AuditableEntity
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
