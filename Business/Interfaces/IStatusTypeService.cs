using Business.Models;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<IEnumerable<StatusType>> GetAllStatusTypesAsync();
}
