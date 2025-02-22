using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    //Does not Inlude Project list
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        //TO DO: Change name of method
        //No need to do .Include on Projects here (Customers list will be used in ComboBox)
        var entities = await _customerRepository.GetAllAsyncWithQuery();
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers != null && customers.Any() ? customers : [];
    }

    /* Original version - before impl Queryable in BaseRepository
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers != null && customers.Any() ? customers : [];
    }
    */

    //Inludes Project list
    public async Task<Customer?> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        //TO DO: Change name of method
        //Inludes Project list 
        var entity = await _customerRepository.GetAsyncWithQuery(expression, query =>
            query.Include(x => x.Projects)
            );
      
        var customer = CustomerFactory.Create(entity);
        return customer != null ? customer : null;
    }

    //Does not Inlude Project list
    public async Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        //TO DO: Change name of method
        //Does not Inlude Project list 
        var customer = await _customerRepository.GetAsyncWithQuery(expression);
        return customer;
    }
}
