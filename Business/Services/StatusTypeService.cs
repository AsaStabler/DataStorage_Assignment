using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    //Does not Include Project list in the returned StatusTypes
    //Method is used to populate ComboBox - Project list not needed
    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        try
        {
            //TO DO: Change name of method
            var entities = await _statusTypeRepository.GetAllAsyncWithQuery();
            var statusTypes = entities.Select(StatusTypeFactory.Create).ToList();
            return statusTypes != null && statusTypes.Any() ? statusTypes : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Services :: {ex.Message}");
            return [];
        }
    }

    //Does not Include Project list in returned StatusTypeEntity
    public async Task<StatusTypeEntity?> GetStatusTypeEntityAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {
        try
        {
            //TO DO: Change name of method
            var statusType = await _statusTypeRepository.GetAsyncWithQuery(expression);
            return statusType;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one StatusTypeEntity :: {ex.Message}");
            return null;
        }
    }
}
