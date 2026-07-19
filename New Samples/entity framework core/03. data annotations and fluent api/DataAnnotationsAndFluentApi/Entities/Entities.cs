using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationsAndFluentApi.Entities;

public enum EmploymentStatus
{
    Active,
    OnLeave,
    Terminated
}

// [Table] maps this class to a different table name
[Table("Employees")]
public class Employee
{
    [Key] // Marks the primary key (EF also auto-detects "Id" by convention)
    public int Id { get; set; }

    [Required]          // NOT NULL in DB
    [MaxLength(100)]    // nvarchar(100)
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(200)]
    public string? Email { get; set; }

    // [Column] renames the DB column
    [Column("hire_date")]
    public DateTime HireDate { get; set; }

    // Enum stored as string — configured via Fluent API below
    public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;

    // Foreign key to Department
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    // Owned type — Address columns are embedded in the Employees table
    public Address HomeAddress { get; set; } = null!;
}

// Department entity — configured entirely via Fluent API (no annotations)
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

// Owned type — not a table itself, its columns live inside the owning entity's table
public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
