using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static Customer Create(CustomerEntity entity)
    {
        return new Customer
        {
            //NOTE! Denna metod används för att mappa en lista med CustomerEntity från databasen
            //till en lista med Customer. Dessa ska sedan visas i en drop-down på Projectsidan.
            //Behövs då Email resp. Phone mappas in i modellen Customer? Om de nu ändå inte ska visas?

            Id = entity.Id,
            CustomerName = entity.CustomerName,
            CustomerEmail = entity.CustomerEmail,  // ???
            CustomerPhone = entity.CustomerPhone,  // ???
        };
    }
}
