using Business.DTOs;
using Business.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project?> UpdateProjectAsync(int Id, ProjectUpdateForm form);
    Task<bool> DeleteProjectByIdAsync(int Id);
    //Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> expression);
}