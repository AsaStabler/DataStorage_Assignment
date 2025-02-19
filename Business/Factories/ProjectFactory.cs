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
            EndDate = Convert.ToDateTime(form.EndDate),
            CustomerId = form.CustomerId,
            StatusId = form.StatusId,
            UserId = form.UserId,
            ServiceId = form.ServiceId,
            Total = form.Total,
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
            //TO DO: Hantera null värden av DateTime från databasen, dvs 0001-01-01
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

            Total = entity.Total,
        };
    }

    public static ProjectEntity Update(ProjectEntity existingProject, ProjectUpdateForm updateForm)
    {
       return existingProject;
    }

}
