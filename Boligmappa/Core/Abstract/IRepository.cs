using System.Linq.Expressions;
using Core.Entities;

namespace Core.Abstract;

public interface IEntityRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Remove(T entity);
    Task SaveChangesAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

    
}