using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    //Does not Inlude Project list
    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        //TO DO: Change name of method
        //No need to do .Include on Projects here (Services list will be used in ComboBox)
        var entities = await _serviceRepository.GetAllAsyncWithQuery();
        var services = entities.Select(ServiceFactory.Create).ToList();
        return services != null && services.Any() ? services : [];
    }

    //Does not Inlude Project list
    public async Task<ServiceEntity?> GetServiceEntityAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        //TO DO: Change name of method
        //Does not Inlude Project list 
        var service = await _serviceRepository.GetAsyncWithQuery(expression);
        return service;
    }
}
