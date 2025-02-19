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
    // UPDATE  
    public async Task<Project?> UpdateProjectAsync(int Id, ProjectUpdateForm updateForm)
    {
        var existingProject = await GetProjectEntityAsync(x => x.Id == Id);
        if (existingProject == null)
            return null;

        //ev. TO DO - alt. för att flytta ut all den här koden från ProjectServic
        //existingProject = ProjectFactory(existingProject, updateForm)

        existingProject.Title = string.IsNullOrWhiteSpace(updateForm.Title) ? existingProject.Title : updateForm.Title;
        existingProject.Description = updateForm.Description;  //Description can be updated to null or empty

        if (existingProject.StartDate != updateForm.StartDate) existingProject.StartDate = Convert.ToDateTime(updateForm.StartDate);
        if (existingProject.EndDate != updateForm.EndDate) existingProject.EndDate = Convert.ToDateTime(updateForm.EndDate);

        if (existingProject.CustomerId != updateForm.CustomerId  && updateForm.CustomerId != 0) 
            existingProject.CustomerId = updateForm.CustomerId;
        if (existingProject.StatusId != updateForm.StatusId && updateForm.StatusId != 0) 
            existingProject.StatusId = updateForm.StatusId;
        if (existingProject.UserId != updateForm.UserId && updateForm.UserId != 0) 
            existingProject.UserId = updateForm.UserId;
        if (existingProject.ServiceId != updateForm.ServiceId && updateForm.ServiceId != 0) 
            existingProject.ServiceId = updateForm.ServiceId;

        //TO DO: Test the conversion OR remove Total property
        //if (existingProject.Total != form.Total) existingProject.Total = (decimal)form.Total!;   

        var result = await _projectRepository.UpdateAsync(x => x.Id == Id, existingProject);
       
        return (result != null) ? ProjectFactory.Create(existingProject) : null;
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
