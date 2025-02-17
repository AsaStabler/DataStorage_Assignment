using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    //Inför Try - Catch här
    // CREATE
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        //To Do ***
        //var customer = await GetCustomerEntityAsync(x => x.Email == form.Email);
        //if (customer != null)
        //    return false;

        //NOTE! BaseRepository.CreateAsync returnerar det skapade objektet, inte true-false
        var result = await _projectRepository.CreateAsync(ProjectFactory.Create(form));
        //return result;
        return true;   //Ska ändras!!!
    }

    //Inför Try - Catch här
    // READ
    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create).ToList();
        return projects != null && projects.Any() ? projects : [];
    }

    //Inför Try - Catch här
    // UPDATE   *** NOT COMPLETED ***
    public async Task<Project?> UpdateProjectAsync(int Id, ProjectUpdateForm form)
    {
        var existingProject = await GetProjectEntityAsync(x => x.Id == Id);
        if (existingProject == null)
            return null;

        existingProject.Title = string.IsNullOrWhiteSpace(form.Title) ? existingProject.Title : form.Title;
        existingProject.Description = string.IsNullOrWhiteSpace(form.Description) ? existingProject.Description : form.Description;
        //existingProject.StartDate = string.IsNullOrWhiteSpace(form.StartDate) ? existingProject.StartDate : form.StartDate;
        //existingProject.EndDate = string.IsNullOrWhiteSpace(form.EndDate) ? existingProject.EndDate : form.EndDate;

        /*** Cannot convert from ´int´ to ´string´  ***/
        //existingProject.CustomerId = string.IsNullOrWhiteSpace(form.CustomerId) ? existingProject.CustomerId : form.CustomerId;
        //existingProject.StatusId = string.IsNullOrWhiteSpace(form.StatusId) ? existingProject.StatusId : form.StatusId;
        //existingProject.UserId = string.IsNullOrWhiteSpace(form.UserId) ? existingProject.UserId : form.UserId;
        //existingProject.ServiceId = string.IsNullOrWhiteSpace(form.ServiceId) ? existingProject.ServiceId : form.ServiceId;
        //existingProject.Total = string.IsNullOrWhiteSpace(form.Total) ? existingProject.Total : form.Total;

        if (existingProject.CustomerId != form.CustomerId) existingProject.CustomerId = form.CustomerId;
        if (existingProject.StatusId != form.StatusId) existingProject.StatusId = form.StatusId;
        if (existingProject.UserId != form.UserId) existingProject.UserId = form.UserId;
        if (existingProject.ServiceId != form.ServiceId) existingProject.ServiceId = form.ServiceId;

        //if (existingProject.Total != form.Total) existingProject.Total = (decimal)form.Total!;   //TO DO: Test the conversion 

        var result = await _projectRepository.UpdateAsync(x => x.Id == Id, existingProject);

        if (result != null)
            return ProjectFactory.Create(existingProject);
        else
            return null;
        //return result ? ProjectFactory.Create(existingProject) : null;
    }

    //Inför Try - Catch här
    // DELETE
    public async Task<bool> DeleteProjectByIdAsync(int Id)
    {
        //Den här kontrollen görs också i BaseRepository  ***
        var project = await GetProjectEntityAsync(x => x.Id == Id);
        if (project == null)
            return false;

        var result = await _projectRepository.DeleteAsync(x => x.Id == Id);
        return result;
    }

    //Inför Try - Catch här (enbart om metoden görs om till public, om den förblir private så behövs det inte).
    private async Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var project = await _projectRepository.GetAsync(expression);
        return project;
    }
}
