using System.Linq.Expressions;

namespace SchoolApi.Repos;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<IQueryable<TEntity>> GetAllWithIncludesAsync(params string[] includes);
    Task<TEntity> GetByIdAsync(object id);
    Task<TEntity> GetByIdWithIncludesAsync(object id, params string [] includes);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(object id);
}
