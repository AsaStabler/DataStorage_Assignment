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
            StartDate = DateTime.Now,  // TO DO: detta är tillfällig lösning
            EndDate = DateTime.Now,    // TO DO: detta är tillfällig lösning
            //StartDate = form.StartDate,
            //EndDate = form.EndDate,
            CustomerId = form.CustomerId,
            StatusId = form.StatusId,
            UserId = form.UserId,
            ServiceId = form.ServiceId,
            Total = form.Total,
        };
    }

    public static Project Create(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description!, 
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,

            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer.CustomerName,

            StatusId = entity.StatusId,
            StatusName = entity.Status.StatusName,

            //TO DO: UserDisplayName could be declared in a User model (or factory?)
            UserId = entity.UserId,
            UserDisplayName = entity.User.FirstName + " " + entity.User.LastName,

            //TO DO: ServiceDescription could be declared in a Service model (or factory?)
            ServiceId = entity.ServiceId,
            ServiceDescription = entity.Service.ServiceName + " (" + entity.Service.Price + " kr/h)",

            Total = entity.Total,
        };
    }


}
