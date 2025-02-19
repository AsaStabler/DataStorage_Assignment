using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var entities = await _userRepository.GetAllAsync();
        var users = entities.Select(UserFactory.Create).ToList();
        return users != null && users.Any() ? users : [];
    }
}