using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    // READ 
    /*
    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Projects
                .Include(c => c.Customer)
                .Include(s => s.Status)
                .Include(u => u.User)
                .Include(se => se.Service)
                .ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all ProjectEntities :: {ex.Message}");
            return [];
        }
    } */

    /*
    public override async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Projects
                .Include(c => c.Customer)
                .Include(s => s.Status)
                .Include(u => u.User)
                .Include(se => se.Service)
                .FirstOrDefaultAsync(expression);

            return entity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one ProjectEntity :: {ex.Message}");
            return null!;
        }
    }
    */
}
