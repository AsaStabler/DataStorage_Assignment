using Business.DTOs;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(ProjectRegistrationForm form);

    Task<IEnumerable<Project>> GetAllProjectsAsyncWithQuery();
    //Task<IEnumerable<Project>> GetAllProjectsAsync();   //OLD - to be removed

    Task<Project?> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<Project?> GetProjectByIdAsync(int Id);

    Task<bool> UpdateProjectAsync(int Id, ProjectUpdateForm form);
    Task<bool> DeleteProjectByIdAsync(int Id);
}