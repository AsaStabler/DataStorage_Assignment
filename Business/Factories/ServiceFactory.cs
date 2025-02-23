using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServiceFactory
{
    public static Service Create(ServiceEntity entity)
    {
        return new Service
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            Price = entity.Price,
            ServiceDescription = entity.ServiceName + " (" + entity.Price + " kr/h)",
        };
    }
}
