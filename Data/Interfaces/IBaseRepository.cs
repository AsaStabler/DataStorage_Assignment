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

    //Task<TEntity> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    //Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updateEntity);
    //Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);

    Task<IEnumerable<TEntity>> GetAllAsyncWithQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
    Task<TEntity> GetAsyncWithQuery(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
    
}