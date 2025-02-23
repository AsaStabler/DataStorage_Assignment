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

    //Does not Include Project list in the returned Services
    //Method is used to populate ComboBox - Project list not needed
    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        try
        {
            //TO DO: Change name of method
            var entities = await _serviceRepository.GetAllAsyncWithQuery();
            var services = entities.Select(ServiceFactory.Create).ToList();
            return services != null && services.Any() ? services : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Services :: {ex.Message}");
            return [];
        }
    }

    //Does not Include Project list in returned ServiceEntity
    public async Task<ServiceEntity?> GetServiceEntityAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        try
        { 
            //TO DO: Change name of method
            var service = await _serviceRepository.GetAsyncWithQuery(expression);
            return service;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one ServiceEntity :: {ex.Message}");
            return null;
        }
    }
}
