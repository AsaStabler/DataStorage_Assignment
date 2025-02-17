using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    //protected readonly DataContext _context = context;
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    // CREATE
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

    // READ
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

    // UPDATE
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

            //_dbSet.Update(entity);
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

    // DELETE
    // *** NOTE: Exempel på hur göra delete med input parameter av HELT OBJEKT (inte expression) ***
    //public virtual async Task<bool> RemoveAsync(TEntity entity) //Tors 30/1
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
    }


    public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
}
