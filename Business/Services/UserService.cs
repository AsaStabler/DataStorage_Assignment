using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    //Does not Include Project list in the returned Users
    //Method is used to populate ComboBox - Project list not needed
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        { 
            //TO DO: Change name of method
            //No need to do .Include on Projects here (Users list will be used in ComboBox)
            var entities = await _userRepository.GetAllAsyncWithQuery();
            var users = entities.Select(UserFactory.Create).ToList();
            return users != null && users.Any() ? users : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Users :: {ex.Message}");
            return [];
        }
    }

    //Does not Include Project list in returned UserEntity
    public async Task<UserEntity?> GetUserEntityAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            //TO DO: Change name of method
            //Does not Inlude Project list 
            var user = await _userRepository.GetAsyncWithQuery(expression);
            return user;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one UserEntity :: {ex.Message}");
            return null;
        }
    }

}