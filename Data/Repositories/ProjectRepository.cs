using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    // CREATE
    //Can use CreateAsync method in BaseRepository as it is

    // READ    -    THIS WILL HAVE TO BE OVERRIDDEN DUE TO 4 FOREIGN KEYS...FÖLJER VIDEON OM BASEREPOSITORY
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
    }

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

    // UPDATE 
   /*   WILL PROBABLY NOT HAVE TO BE OVERRIDDEN
   public async Task<bool> UpdateOneAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity)
   {
       try
       {
           var existingEntity = await _context.Projects.FirstOrDefaultAsync(expression);
           if (existingEntity != null && updatedEntity != null)
           {
               _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
               await _context.SaveChangesAsync();
               return true;
           }
           return false;
       }
       catch (Exception ex)
       {
           Debug.WriteLine(ex.Message);
           return false;
       }
   } */

    // DELETE
    /*   WILL PROBABLY NOT HAVE TO BE OVERRIDDEN
    public override async Task<bool> DeleteAsync(ProjectEntity entity)
    {
        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;

        }
    } */
}
