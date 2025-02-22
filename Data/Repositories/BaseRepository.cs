using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction _transaction = null!;

    #region Transaction Management

    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }

    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    #endregion

    #region CRUD

    // CREATE
    // (ADD, has to be followed by a SAVE)
    public virtual async Task AddAsync(TEntity entity)
    { 
        await _dbSet.AddAsync(entity);
    }

    // SAVE (for Transaction Management)
    //Can not have try-catch here, since it would circumvent the catch-statment in the Service
    //and then Rollback would not be performed
    public virtual async Task<int> SaveAsync()
    { 
        return await _context.SaveChangesAsync();
    }

    // UPDATE , has to be followed by a SAVE)
    public virtual void Update(TEntity updateEntity)
    {
        _dbSet.Update(updateEntity);
    }

    // DELETE, has to be followed by a SAVE)
    public virtual void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    // OLD CREATE - W/O Transaction Management
    /*
    public virtual async Task<TEntity> CreateAsync(TEntity entity)  //Ev returnera bool istället
    {
        if (entity == null)
            return null!;

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} entity :: {ex.Message}");
            return null!;
        }
    }
    */

    // READ
    public virtual async Task<IEnumerable<TEntity>> GetAllAsyncWithQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null) 
    {
        IQueryable<TEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        var entities = await query.ToListAsync();
        return entities;
    }

    public virtual async Task<TEntity> GetAsyncWithQuery(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null) 
    {
        if (expression == null)
            return null!;

        IQueryable<TEntity> query = _dbSet;

        if (includeExpression != null)
            query = includeExpression(query);

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity!;
    }


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all {nameof(TEntity)} entity :: {ex.Message}");
            return [];
        }
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    //public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            return entity!;
            //return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one {nameof(TEntity)} entity :: {ex.Message}");
            return null!;
        }
    }

    /*
    //OLD UPDATE - W/O Transaction Management
    //public virtual async Task<bool> UpdateAsync(TEntity entity) //Retur av en bool
    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updateEntity)
    {
        if (updateEntity == null)
            return null!;

        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
            if (existingEntity == null)
                return null!;

            //_dbSet.Update(entity);  KAN TA BORT
            _context.Entry(existingEntity).CurrentValues.SetValues(updateEntity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating {nameof(TEntity)} entity :: {ex.Message}");
            return null!;
        }
    }
    */

    // OLD DELETE - W/O Transaction Management
    // *** NOTE: Exempel på hur göra delete med input parameter av HELT OBJEKT (inte expression) ***
    //public virtual async Task<bool> RemoveAsync(TEntity entity) //Tors 30/1
    /*
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
            if (existingEntity == null)
                return false;

            //_dbSet.Remove(entity);
            _dbSet.Remove(existingEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)} entity :: {ex.Message}");
            return false;

        }
    } */

    public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }

    #endregion
}
