
using Data.Interfaces;
using Data.Repositories;

namespace Data.Services;

public class CustomerTestService(ICustomerRepository customerRepository) : ICustomerTestService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    /*
    public async Task<ResponseResult<IEnumerable<User>>> GetAllUsersAsync()
    {
        var entities = await _userRepository.GetAllAsync();
        var users = entities.Select(UserFactory.Create).ToList();
        return new ResponseResult<IEnumerable<User>>(true, 200, null, users);
    }
    */

    /*
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create).ToList();
        return customers != null && customers.Any() ? customers : [];
    }
    */

}
