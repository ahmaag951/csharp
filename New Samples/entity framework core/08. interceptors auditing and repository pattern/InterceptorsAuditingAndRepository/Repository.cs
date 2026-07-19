using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InterceptorsAuditingAndRepository;

// ── REPOSITORY PATTERN ────────────────────────────────────────
// Wraps EF Core behind an abstraction so that:
//   - Business logic doesn't depend directly on DbContext
//   - You can unit-test business logic by substituting a fake repository

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task SaveAsync();
}

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _db;
    protected readonly DbSet<T> _set;

    public Repository(AppDbContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) =>
        await _set.FindAsync(id);

    public async Task<IReadOnlyList<T>> GetAllAsync() =>
        await _set.ToListAsync();

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _set.Where(predicate).ToListAsync();

    public async Task AddAsync(T entity) =>
        await _set.AddAsync(entity);

    public void Update(T entity) =>
        _set.Update(entity);

    public void Remove(T entity) =>
        _set.Remove(entity);

    public async Task SaveAsync() =>
        await _db.SaveChangesAsync();
}
