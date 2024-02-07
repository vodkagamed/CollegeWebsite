using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SchoolApi.Repos;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        var addedEntity = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public async Task<T> DeleteAsync(object id)
    {
        var entityToRemove = await _dbSet.FindAsync(id);
        if (entityToRemove != null)
        {
            _dbSet.Remove(entityToRemove);
            await _context.SaveChangesAsync();
        }
        return entityToRemove;
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> GetAllWithIncludesAsync(IEnumerable<string> includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

    public async Task<T> GetByIdWithIncludesAsync(object id, IEnumerable<string> includes)
    {
        var query = _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        var entit = await _dbSet.FindAsync(id);
        return (await query.FirstOrDefaultAsync(entity => entity == entit));
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}
