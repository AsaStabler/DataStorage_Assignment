using Business.Models;

namespace Business.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAllServicesAsync();
}