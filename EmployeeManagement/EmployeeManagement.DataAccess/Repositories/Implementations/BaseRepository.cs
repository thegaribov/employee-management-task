using EmployeeManagement.DataAccess.Contexts;
using EmployeeManagement.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace EmployeeManagement.Repositories.Implementations;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected EmployeeManagementDbContext _context;
    internal DbSet<TEntity> _dbSet;
    internal ILogger<BaseRepository<TEntity>> _logger;

    public BaseRepository(
        EmployeeManagementDbContext dbContext,
        ILogger<BaseRepository<TEntity>> logger)
    {
        _context = dbContext;
        _dbSet = _context.Set<TEntity>();
        _logger = logger;
    }

    public virtual IQueryable<TEntity> GetQuery()
    {
        return _context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        var result = _context.Set<TEntity>().AsNoTracking();
        if (expression != null)
            return await result.Where(expression).ToListAsync();

        return await result.ToListAsync();
    }

    public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
    }
    public virtual async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().SingleOrDefaultAsync(expression);
    }

    public virtual void Add(TEntity entity)
    {
        _logger?.LogInformation($"Insert an entity {entity}");
        _context.Set<TEntity>().Add(entity);
    }
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        _logger?.LogInformation($"Add range entities {entities}");
        _context.Set<TEntity>().AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        _logger?.LogInformation($"Update an entity {entity}");
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _logger?.LogInformation($"Update range entities {entities}");

        _dbSet.AttachRange(entities);
        foreach (var entity in entities)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }

    public virtual void Delete(TEntity entity)
    {
        _logger?.LogInformation($"Delete an entity {entity}");

        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }
    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
        }

        _dbSet.RemoveRange(entities);
    }
}
