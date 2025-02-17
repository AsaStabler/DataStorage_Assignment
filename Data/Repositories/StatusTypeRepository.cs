using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context) : BaseRepository<StatusTypeEntity>(context), IStatusTypeRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<StatusTypeEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.StatusTypes
                .Include(x => x.Projects)
                .ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all StatusTypeEntities :: {ex.Message}");
            return [];
        }
    }

    public override async Task<StatusTypeEntity> GetAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.StatusTypes
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(expression);
            return entity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one StatusTypeEntity :: {ex.Message}");
            return null!;
        }
    }
}
