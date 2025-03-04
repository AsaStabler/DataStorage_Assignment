﻿using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerService customerService, IStatusTypeService statusTypeService,
                            IUserService userService, IServiceService serviceService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    // CREATE
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        //Validation of input data, including controls that Foreign Keys exist in database
        if (await IsValid(form)) {

            //Transaction Management
            await _projectRepository.BeginTransactionAsync();
            try
            {
                await _projectRepository.AddAsync(ProjectFactory.Create(form));
                await _projectRepository.SaveAsync();
                await _projectRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating a Project :: {ex.Message}");
                await _projectRepository.RollbackTransactionAsync();
                return false;
            }
        } 
        else 
            return false;
    }

    private async Task<bool> IsValid(ProjectRegistrationForm form)
    {
        //Check that ProjectRegistrationForm is not null
        if (form == null) return false;

        //Title can not be null
        if (string.IsNullOrWhiteSpace(form.Title)) return false;

        //StartDate can not be null; if not selected in drop down then it´s value is null here
        //End Date is allowed to be null, i.e. not selected
        if (form.StartDate == null) return false;

        //Check if Foreign Keys exist in database
        var customer = await customerService.GetCustomerEntityAsync(x => x.Id == form.CustomerId);
        var status = await statusTypeService.GetStatusTypeEntityAsync(x => x.Id == form.StatusId);
        var user = await userService.GetUserEntityAsync(x => x.Id == form.UserId);
        var service = await serviceService.GetServiceEntityAsync(x => x.Id == form.ServiceId);

        //If any of the Foreign Keys do not exist in db, then Project can not be created
        if (customer == null || status == null || user == null || service == null)
            return false;

        //If all data in ProjectRegistrationForm is valid, then return true
        return true;
    }

    private async Task<bool> IsValidForUpdate(ProjectUpdateForm form)
    {
        //Check that ProjecUpdateForm is not null
        if (form == null) return false;

        //Title can not be null
        if (string.IsNullOrWhiteSpace(form.Title)) return false;

        //StartDate can not be null; if not selected in drop down then it´s value is null here
        //End Date is allowed to be null, i.e. not selected
        if (form.StartDate == null) return false;

        //Check if Foreign Keys exist in database
        var customer = await customerService.GetCustomerEntityAsync(x => x.Id == form.CustomerId);
        var status = await statusTypeService.GetStatusTypeEntityAsync(x => x.Id == form.StatusId);
        var user = await userService.GetUserEntityAsync(x => x.Id == form.UserId);
        var service = await serviceService.GetServiceEntityAsync(x => x.Id == form.ServiceId);

        //If any of the Foreign Keys do not exist in db, then Project can not be updated
        if (customer == null || status == null || user == null || service == null)
            return false;

        //If all data in ProjectUpdateForm is valid, then return true
        return true;
    }

    // READ
    //With Eager Loading
    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        try
        {
            var entities = await _projectRepository.GetAllAsync(query =>
                query.Include(c => c.Customer)
                    .Include(s => s.Status)
                    .Include(u => u.User)
                    .Include(se => se.Service)
            );

            var projects = entities.Select(ProjectFactory.Create).ToList();
            return projects != null && projects.Any() ? projects : [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all Projects :: {ex.Message}");
            return [];
        }
    }

    //With Eager Loading
    public async Task<Project?> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(expression, query =>
            query.Include(c => c.Customer)
                .Include(s => s.Status)
                .Include(u => u.User)
                .Include(se => se.Service)
            );

            var project = ProjectFactory.Create(entity!);
            return project != null ? project : null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one Project :: {ex.Message}");
            return null;
        }
    }

    //With Eager Loading
    public async Task<Project?> GetProjectByIdAsync(int projectId)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(x => x.Id == projectId, query =>
            query.Include(c => c.Customer)
                .Include(s => s.Status)
                .Include(u => u.User)
                .Include(se => se.Service)
            );
            var project = ProjectFactory.Create(entity!);
            return project != null ? project : null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one Project :: {ex.Message}");
            return null;
        }
    }

    // UPDATE  
    public async Task<bool> UpdateProjectAsync(int projectId, ProjectUpdateForm updateForm)
    {
        //If existingProject does not exist in db, then do not proceed with Update
        var existingProject = await GetProjectEntityAsync(x => x.Id == projectId);
        if (existingProject == null)
            return false;

        //Validation of input data, including controls that Foreign Keys exist in database
        if (!await IsValidForUpdate(updateForm)) return false;

        //Map updateForm to existingProject
        existingProject = ProjectFactory.Create(existingProject, updateForm);

        //Transaction Management
        await _projectRepository.BeginTransactionAsync();
        try
        {
            _projectRepository.Update(existingProject);
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating a Project :: {ex.Message}");
            await _projectRepository.RollbackTransactionAsync();
            return false;
        }
    }

    // DELETE
    public async Task<bool> DeleteProjectByIdAsync(int Id)
    {
        //If existingProject does not exist in db, then do not proceed with Delete
        var projectEntity = await GetProjectEntityAsync(x => x.Id == Id);
        if (projectEntity == null)
            return false;

        //Transaction Management
        await _projectRepository.BeginTransactionAsync();
        try
        {
            _projectRepository.Remove(projectEntity);
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting a Project :: {ex.Message}");
            await _projectRepository.RollbackTransactionAsync();
            return false;
        }
    }

    //Without Eager Loading
    private async Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        try
        {
            var project = await _projectRepository.GetAsync(expression); 
            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting one ProjectEntity :: {ex.Message}");
            return null;
        }
    }
}
