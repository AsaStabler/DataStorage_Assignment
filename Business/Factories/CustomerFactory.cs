using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static Customer Create(CustomerEntity entity)
    {
        return new Customer
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            CustomerEmail = entity.CustomerEmail,  
            CustomerPhone = entity.CustomerPhone,  
        };
    }
}
