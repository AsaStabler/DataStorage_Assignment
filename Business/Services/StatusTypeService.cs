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

    //Without Eager Loading (use method to populate ComboBoxes)
    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        try
        {
            var entities = await _statusTypeRepository.GetAllAsync();
            var statusTypes = entities.Select(StatusTypeFactory.Create).ToList();
            return statusTypes != null && statusTypes.Any() ? statusTypes : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Services :: {ex.Message}");
            return [];
        }
    }

    //Without Eager Loading
    public async Task<StatusTypeEntity?> GetStatusTypeEntityAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {
        try
        {
            var statusType = await _statusTypeRepository.GetAsync(expression);
            return statusType;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one StatusTypeEntity :: {ex.Message}");
            return null;
        }
    }
}
