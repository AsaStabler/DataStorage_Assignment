using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAllServicesAsync();
    Task<ServiceEntity?> GetServiceEntityAsync(Expression<Func<ServiceEntity, bool>> expression);
}