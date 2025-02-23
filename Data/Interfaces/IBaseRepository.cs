using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    Task AddAsync(TEntity entity);
    void Update(TEntity updateEntity);
    void Remove(TEntity entity);
    Task<int> SaveAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);

    Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
}