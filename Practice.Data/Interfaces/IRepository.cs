using System.Linq.Expressions;

namespace Practice.Data.Interfaces;

public interface IRepository<T> where T : class
{
    // Create
    Task<T> CreateAsync(T entity);
    Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);

    // Read
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    // Update
    Task<T> UpdateAsync(T entity);
    Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

    // Delete
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteAsync(T entity);
    Task<int> DeleteRangeAsync(IEnumerable<T> entities);
    Task<int> DeleteRangeAsync(Expression<Func<T, bool>> predicate);
}
