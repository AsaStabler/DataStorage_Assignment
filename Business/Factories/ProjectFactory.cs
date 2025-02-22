using Business.DTOs;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        return new ProjectEntity
        {
            Title = form.Title,
            Description = form.Description!,
            StartDate = Convert.ToDateTime(form.StartDate),
            EndDate = form.EndDate,     //EndDate is allowed to be null
            CustomerId = form.CustomerId,
            StatusId = form.StatusId,
            UserId = form.UserId,
            ServiceId = form.ServiceId,
        };
    }

    //Går inte att sätta StartDate = form.StartDate, eftersom StartDate i ProjectRegistrationForm
    //är satt till "nullabel", dvs DateTime?
    //Därför måste konverteringen ske här, dvs Convert.ToDateTime(form.StartDate),
    //och detta fungerar oavsett om StarDate har ett faktiskt DateTime värde eller om det är null
    //OM StartDate är null så ger konverteringen värdet 0001-01-01 och detta skrivs in i databasen

    public static Project Create(ProjectEntity entity)
    {
        //TO DO - Ska rensa/ordna om i modellen Project *********
        return new Project
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description!,
           
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,

            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer.CustomerName,
            Customer = CustomerFactory.Create(entity.Customer),

            StatusId = entity.StatusId,
            StatusName = entity.Status.StatusName,

            //TO DO: UserDisplayName could be declared in a User model (or factory?)
            UserId = entity.UserId,
            UserDisplayName = entity.User.FirstName + " " + entity.User.LastName,

            //TO DO: ServiceDescription could be declared in a Service model (or factory?)
            ServiceId = entity.ServiceId,
            ServiceDescription = entity.Service.ServiceName + " (" + entity.Service.Price + " kr/h)",
        };
    }

    public static ProjectEntity MapForUpdate(ProjectEntity existingProject, ProjectUpdateForm updateForm)
    {
        existingProject.Title = string.IsNullOrWhiteSpace(updateForm.Title) ? existingProject.Title : updateForm.Title;
        //Description is allowed to be null or empty
        existingProject.Description = updateForm.Description;

        if (existingProject.StartDate != updateForm.StartDate) existingProject.StartDate = Convert.ToDateTime(updateForm.StartDate);
        //End Date is allowed to be NULL
        if (existingProject.EndDate != updateForm.EndDate) existingProject.EndDate = updateForm.EndDate;

        if (existingProject.CustomerId != updateForm.CustomerId && updateForm.CustomerId != 0)
            existingProject.CustomerId = updateForm.CustomerId;
        if (existingProject.StatusId != updateForm.StatusId && updateForm.StatusId != 0)
            existingProject.StatusId = updateForm.StatusId;
        if (existingProject.UserId != updateForm.UserId && updateForm.UserId != 0)
            existingProject.UserId = updateForm.UserId;
        if (existingProject.ServiceId != updateForm.ServiceId && updateForm.ServiceId != 0)
            existingProject.ServiceId = updateForm.ServiceId;

        return existingProject;
    }

}
