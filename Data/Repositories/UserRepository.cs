using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Users
                .Include(x => x.Projects)
                    .ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all UserEntities :: {ex.Message}");
            return [];
        }
    }

    public override async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Users
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(expression);
            return entity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one UserEntity :: {ex.Message}");
            return null!;
        }
    }
}