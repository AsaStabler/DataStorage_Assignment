using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    /*
    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Customers
                .Include(x => x.Projects)
                .ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all CustomerEntities :: {ex.Message}");
            return [];
        }
    }
    */

    /*
    public override async Task<CustomerEntity> GetAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Customers
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(expression);
            return entity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one CustomerEntity :: {ex.Message}");
            return null!;
        }
    }
    */
}
