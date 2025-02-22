using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    //Does not Inlude Project list
    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        //TO DO: Change name of method
        //No need to do .Include on Projects here (StatusTypes list will be used in ComboBox)
        var entities = await _statusTypeRepository.GetAllAsyncWithQuery();
        var statusTypes = entities.Select(StatusTypeFactory.Create).ToList();
        return statusTypes != null && statusTypes.Any() ? statusTypes : [];
    }

    //Does not Inlude Project list
    public async Task<StatusTypeEntity?> GetStatusTypeEntityAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {
        //TO DO: Change name of method
        //Does not Inlude Project list 
        var statusType = await _statusTypeRepository.GetAsyncWithQuery(expression);
        return statusType;
    }
}
