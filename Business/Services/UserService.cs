using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    //Does not Inlude Project list
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        //TO DO: Change name of method
        //No need to do .Include on Projects here (Users list will be used in ComboBox)
        var entities = await _userRepository.GetAllAsyncWithQuery();
        var users = entities.Select(UserFactory.Create).ToList();
        return users != null && users.Any() ? users : [];
    }

    //Does not Inlude Project list
    public async Task<UserEntity?> GetUserEntityAsync(Expression<Func<UserEntity, bool>> expression)
    {
        //TO DO: Change name of method
        //Does not Inlude Project list 
        var user = await _userRepository.GetAsyncWithQuery(expression);
        return user;
    }

}