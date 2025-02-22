using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<IEnumerable<StatusType>> GetAllStatusTypesAsync();
    Task<StatusTypeEntity?> GetStatusTypeEntityAsync(Expression<Func<StatusTypeEntity, bool>> expression);
}
