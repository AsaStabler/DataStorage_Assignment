using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static StatusType Create(StatusTypeEntity entity)
    {
        return new StatusType
        {   
            Id = entity.Id,
            StatusName = entity.StatusName,
        };
    }
}
