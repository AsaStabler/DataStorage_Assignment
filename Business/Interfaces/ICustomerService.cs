using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> expression);
}
