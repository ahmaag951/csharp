using System.ComponentModel.DataAnnotations;

namespace TransactionsConcurrencyAndResiliency.Entities;

public class BankAccount
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Owner { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    // [Timestamp] tells EF to use this column as a concurrency token
    // SQL Server automatically updates it on every row change (rowversion)
    // EF will throw DbUpdateConcurrencyException if it changed between read and save
    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}

public class Transaction
{
    public int Id { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
}
