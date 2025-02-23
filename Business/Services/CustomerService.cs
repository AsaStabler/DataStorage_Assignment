using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    //Without Eager Loading (use method to populate ComboBoxes)
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        try 
        { 
            var entities = await _customerRepository.GetAllAsync(); 
            var customers = entities.Select(CustomerFactory.Create).ToList();
            return customers != null && customers.Any() ? customers : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Customers :: {ex.Message}");
            return [];
        }
    }

    //With Eager Loading
    public async Task<Customer?> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var entity = await _customerRepository.GetAsync(expression, query =>
            query.Include(x => x.Projects)
            );

            var customer = CustomerFactory.Create(entity!);
            return customer != null ? customer : null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one Customer :: {ex.Message}");
            return null;
        }

    }

    //Without Eager Loading
    public async Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var customer = await _customerRepository.GetAsync(expression); 
            return customer;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one CustomerEntity :: {ex.Message}");
            return null;
        }
    }
}
