﻿using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    private readonly DataContext _context = context;

    // All of the CRUD functionality is handled by BaseRepository
    //BUT need to override GetAllAsync och GetAsync här, eftersom CustomerEntity har en property ICollection<ProjectEntity> Projects

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
}
