using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        var entities = await _serviceRepository.GetAllAsync();
        var services = entities.Select(ServiceFactory.Create).ToList();
        return services != null && services.Any() ? services : [];
    }
}
