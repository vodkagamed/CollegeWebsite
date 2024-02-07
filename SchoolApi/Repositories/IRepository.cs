using System.Linq.Expressions;

namespace SchoolApi.Repos;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(IEnumerable<string> includes);
    Task<TEntity> GetByIdAsync(object id);
    Task<TEntity> GetByIdWithIncludesAsync(object id, IEnumerable<string> includes);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(object id);
}
