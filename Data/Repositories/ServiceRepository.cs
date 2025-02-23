using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
    /*
    public override async Task<IEnumerable<ServiceEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Services
                .Include(x => x.Projects)
                    .ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all ServiceEntities :: {ex.Message}");
            return [];
        }
    }
    */

    /*
    public override async Task<ServiceEntity> GetAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Services
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(expression);
            return entity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one ServiceEntity :: {ex.Message}");
            return null!;
        }
    }
    */
}
