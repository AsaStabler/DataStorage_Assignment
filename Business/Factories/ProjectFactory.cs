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
            EndDate = form.EndDate,        //EndDate can be null in db
            CustomerId = form.CustomerId,
            StatusId = form.StatusId,
            UserId = form.UserId,
            ServiceId = form.ServiceId,
        };
    }

    public static Project Create(ProjectEntity entity)
    {
        string endDateStr = null!;

        if (entity.EndDate.HasValue)
            endDateStr = entity.EndDate.Value.ToShortDateString();

        return new Project
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description!,

            StartDate = entity.StartDate,
            EndDate = entity.EndDate,

            DisplayStartDate = entity.StartDate.ToString("yyyy-MM-dd"),
            DisplayEndDate = endDateStr,

            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer.CustomerName,

            StatusId = entity.StatusId,
            StatusName = entity.Status.StatusName,

            UserId = entity.UserId,
            UserDisplayName = entity.User.FirstName + " " + entity.User.LastName,

            ServiceId = entity.ServiceId,
            ServiceDescription = entity.Service.ServiceName + " (" + entity.Service.Price + " kr/h)",
        };
    }

    public static ProjectEntity Create(ProjectEntity existingEntity, ProjectUpdateForm updateForm)
    {
        //All data in updateForm has already been validate in method IsValidForUpdate
        existingEntity.Title = updateForm.Title;
        existingEntity.Description = updateForm.Description;
        existingEntity.StartDate = Convert.ToDateTime(updateForm.StartDate);
        existingEntity.EndDate = updateForm.EndDate;    //EndDate can to be null in db
        existingEntity.CustomerId = updateForm.CustomerId;
        existingEntity.StatusId = updateForm.StatusId;
        existingEntity.UserId = updateForm.UserId;
        existingEntity.ServiceId = updateForm.ServiceId;

        return existingEntity;
    }

}
