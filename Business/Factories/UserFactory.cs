using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    public static User Create(UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            UserDisplayName = entity.FirstName + " " + entity.LastName,
        };
    }
}
