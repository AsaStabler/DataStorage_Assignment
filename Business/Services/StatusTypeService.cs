using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        var entities = await _statusTypeRepository.GetAllAsync();
        var statusTypes = entities.Select(StatusTypeFactory.Create).ToList();
        return statusTypes != null && statusTypes.Any() ? statusTypes : [];
    }
}
