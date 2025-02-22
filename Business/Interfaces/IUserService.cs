using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<UserEntity?> GetUserEntityAsync(Expression<Func<UserEntity, bool>> expression);
}
