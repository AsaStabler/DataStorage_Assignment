using Business.DTOs;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace PresentationConsoleMainApp.Dialogs;

public class MenuDialog(ICustomerService customerService, IProjectService projectService) : IMenuDialog
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectService _projectService = projectService;


    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("### Menu ###");
            Console.WriteLine("");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. Get Projects");
            Console.WriteLine("3. Update Project");
            Console.WriteLine("4. Delete Project");
            Console.WriteLine("5. Get Customers");
            Console.Write("Select your option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    //await CreateProjectOption();
                    break;

                case "2":
                    //await GetProjectsOption();
                    break;

                case "3":
                    //await UpdateProjectOption();
                    break;

                case "4":
                    //await DeleteProjectOption();
                    break;

                case "5":
                    //await GetCustomersOption();
                    break;

                default:
                    break;
            }
        }
    }

    //private async Task CreateProjectOption()
    //{
    //    Console.Clear();
    //    Console.WriteLine("### Create New Project ###");
    //    Console.WriteLine("");

    //    Console.Write("Title: ");
    //    var title = Console.ReadLine()!;

    //    Console.Write("Description: ");
    //    var description = Console.ReadLine()!;

    //    Console.Write("Start Date: ");
    //    var startDate = Console.ReadLine()!;

    //    Console.Write("End Date: ");
    //    var endDate = Console.ReadLine();

    //    Console.Write("Totalpris: ");
    //    var total = Console.ReadLine();

    //    var newProject = new ProjectRegistrationForm
    //    {
    //        Title = title,
    //        Description = description,
    //        StartDate = startDate,
    //        EndDate = endDate!,
    //        CustomerId = 1,        
    //        StatusId = 1,
    //        UserId = 3,
    //        ServiceId = 1,
    //        Total = System.Convert.ToInt32(total),   //TO DO: Need to test converting string to decimal
    //    };

    //    var result = await _projectService.CreateProjectAsync(newProject);
    //    if (result)
    //    {
    //        Console.WriteLine("Project was created successfully");
    //    }
    //    else
    //    {
    //        Console.WriteLine("Project was not created");
    //    }

    //    Console.ReadKey();
    //}

    //private async Task GetProjectsOption()
    //{
    //    Console.Clear();
    //    Console.WriteLine("### List of Projects ###");
    //    Console.WriteLine("");

    //    var projects = await _projectService.GetAllProjectsAsync();
    //    if (projects != null && projects.Any())
    //    {
    //        foreach (var project in projects)
    //        {
    //            Console.WriteLine($"{project.Id} {project.Title} {project.Description} {project.StartDate} {project.EndDate} " +
    //                $"{project.CustomerName} {project.StatusName} {project.UserDisplayName} {project.ServiceDescription} {project.Total}");
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("No Projects found");
    //    }

    //    Console.ReadKey();
    //}

    //private async Task UpdateProjectOption()
    //{
    //    Console.Clear();
    //    Console.WriteLine("### Update Project ###");
    //    Console.WriteLine("");

    //    var existingProject = await ShowAvailbleProjectsAndSelect();
    //    if (existingProject == null)
    //        return;

    //    Console.WriteLine($"Updating Project: {existingProject.Title} ");

    //    Console.Write($"New Title (current: {existingProject.Title}, leave empty to keep): ");
    //    var title = Console.ReadLine();

    //    Console.Write($"New Description (current: {existingProject.Description}, leave empty to keep): ");
    //    var description = Console.ReadLine();

    //    //Console.Write("Start Date: ");
    //    //var startDate = Console.ReadLine()!;
    //    var startDate = DateTime.Now;           //TO DO: Tillfällig lösning - ska ändras

    //    //Console.Write("End Date: ");
    //    //var endDate = Console.ReadLine();
    //    var endDate = DateTime.Now;             //TO DO: Tillfällig lösning - ska ändras

    //    Console.Write($"New Totalpris (current: {existingProject.Total}, leave empty to keep): ");
    //    var total = Console.ReadLine();
        
    //    //var totalInt = 0;
    //    //if (total != null && total != "")
    //    //{
    //    //    totalInt = System.Convert.ToInt32(total);
    //    //}
    //    //else
    //    //{ 
    //    //}

    //    var updatedForm = new ProjectUpdateForm
    //    {
    //        Title = title,
    //        Description = description,
    //        StartDate = startDate,
    //        EndDate = endDate,
    //        CustomerId = 1,
    //        StatusId = 1,
    //        UserId = 3,
    //        ServiceId = 1,
    //        Total = System.Convert.ToInt32(total),   //TO DO: Need to test converting string to decimal
    //    };

    //    var result = await _projectService.UpdateProjectAsync(existingProject.Id, updatedForm);
    //    if (result != null)
    //    {
    //        Console.WriteLine("Project was updated successfully");
    //    }
    //    else
    //    {
    //        Console.WriteLine("Project was not updated");
    //    }

    //    Console.ReadKey();

    //}

    //private async Task DeleteProjectOption()
    //{
    //    Console.Clear();
    //    Console.WriteLine("### Delete Project ###");
    //    Console.WriteLine("");

    //    var existingProject = await ShowAvailbleProjectsAndSelect();
    //    if (existingProject == null)
    //        return;

    //    Console.WriteLine($"Are you sure you want to delete Project: {existingProject.Title} ? (y/n): ");
    //    var option = Console.ReadLine();

    //    if (option?.ToLower() == "y")
    //    {
    //        var result = await _projectService.DeleteProjectByIdAsync(existingProject.Id);

    //        if (result)
    //        {
    //            Console.WriteLine("Project was deleted successfully");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Project was not deleted");
    //        }
    //        Console.ReadKey();
    //    }
    //    else
    //    {
    //        Console.WriteLine("Delete operation cancelled");
    //    }

    //    Console.ReadKey();
    //}

    //private async Task GetCustomersOption()
    //{
    //    Console.Clear();
    //    Console.WriteLine("### List of Customers ###");

    //    var customers = await _customerService.GetAllCustomersAsync();
    //    if (customers != null && customers.Any())
    //    {
    //        foreach (var customer in customers)
    //        {
    //            Console.WriteLine($"{customer.CustomerName} {customer.CustomerEmail} {customer.CustomerPhone}");
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("No Customers found");
    //    }
        
    //    Console.ReadKey();
    //}

    //private async Task<Project?> ShowAvailbleProjectsAndSelect()
    //{
    //    var projects = await _projectService.GetAllProjectsAsync();
    //    if (projects == null || !projects.Any())
    //    {
    //        Console.WriteLine("No projects found to update");
    //        Console.ReadKey();
    //        return null;
    //    }

    //    Console.WriteLine("Available Projects: ");
    //    foreach (var project in projects)
    //    {
    //        Console.WriteLine($"ID: {project.Id} {project.Title} {project.Description} {project.StartDate} {project.EndDate} " +
    //                $"{project.CustomerName} {project.StatusName} {project.UserDisplayName} {project.ServiceDescription} {project.Total}");
    //    }

    //    Console.Write("Enter Project Id to manage:");
    //    if (!int.TryParse(Console.ReadLine(), out var projectId))
    //    {
    //        Console.WriteLine("Invalid Id");
    //        Console.ReadKey();
    //        return null;
    //    }

    //    var existingProject = projects.FirstOrDefault(x => x.Id == projectId);
    //    if (existingProject == null)
    //    {
    //        Console.WriteLine("Project not found");
    //        Console.ReadKey();
    //        return null;
    //    }

    //    return existingProject;
    //}

}