using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    //Without Eager Loading (use method to populate ComboBoxes)
    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        try
        {
            var entities = await _serviceRepository.GetAllAsync();
            var services = entities.Select(ServiceFactory.Create).ToList();
            return services != null && services.Any() ? services : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Services :: {ex.Message}");
            return [];
        }
    }

    //Without Eager Loading
    public async Task<ServiceEntity?> GetServiceEntityAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        try
        { 
            var service = await _serviceRepository.GetAsync(expression);
            return service;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one ServiceEntity :: {ex.Message}");
            return null;
        }
    }
}
