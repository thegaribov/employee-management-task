using System.Linq.Expressions;

namespace EmployeeManagement.Repositories.Abstracts;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetQuery();
    Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);

    void Add(TEntity entityToInsert);
    void AddRange(IEnumerable<TEntity> entitiesToInsert);

    void Update(TEntity entityToUpdate);
    void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);

    void Delete(TEntity entityToDelete);
    void DeleteRange(IEnumerable<TEntity> entitiesToDelete);
}
