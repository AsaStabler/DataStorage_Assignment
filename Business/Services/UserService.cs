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

    //Without Eager Loading (use method to populate ComboBoxes)
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        { 
            var entities = await _userRepository.GetAllAsync();
            var users = entities.Select(UserFactory.Create).ToList();
            return users != null && users.Any() ? users : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Users :: {ex.Message}");
            return [];
        }
    }

    //Without Eager Loading
    public async Task<UserEntity?> GetUserEntityAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            var user = await _userRepository.GetAsync(expression);
            return user;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one UserEntity :: {ex.Message}");
            return null;
        }
    }

}