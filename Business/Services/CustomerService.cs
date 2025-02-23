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

    //Does not Include Project list in the returned Customers
    //Method is used to populate ComboBox - Project list not needed
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        try 
        { 
            //TO DO: Change name of method
            //var entities = await _customerRepository.GetAllAsyncWithQuery();
            var entities = await _customerRepository.GetAllAsync();  //Testing!!!
            var customers = entities.Select(CustomerFactory.Create).ToList();
            return customers != null && customers.Any() ? customers : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Customers :: {ex.Message}");
            return [];
        }
    }

    /* OLD version - before impl Queryable in BaseRepository
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers != null && customers.Any() ? customers : [];
    }
    */

    //Includes Project list in returned Customer
    public async Task<Customer?> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            //TO DO: Change name of method
            var entity = await _customerRepository.GetAsyncWithQuery(expression, query =>
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

    //Does not Include Project list in returned CustomerEntity
    public async Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            //TO DO: Change name of method
            //var customer = await _customerRepository.GetAsyncWithQuery(expression);
            var customer = await _customerRepository.GetAsync(expression);   //Testing!!!
            return customer;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one CustomerEntity :: {ex.Message}");
            return null;
        }
    }
}
