using Business.DTOs;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace PresentationWPFMainApp.ViewModels;

public partial class EditProjectViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private string headline = "Edit Project";

    //The original Project to be edited in EditProjectView
    [ObservableProperty]
    private Project _project = new();

    //The ProjectUpdateForm with updated values from EditProjectForm
    [ObservableProperty] 
    private ProjectUpdateForm _updateProject = new(); 

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    [ObservableProperty]
    private Customer _selectedCustomer = new();


    public EditProjectViewModel(IServiceProvider serviceProvider, IProjectService projectService, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _customerService = customerService;

        //Populates the Customer list from the database; to be used in drop down list
        Task.Run(GetCustomersAsync);

        //Set SelectedCustomer from Project that was sent from ProjectsViewModel (in order to show correctly in Customer combobox)
        //SelectedCustomer = Project.Customer;
    }

    public async Task GetCustomersAsync()
    {
        var tempCustomers = await _customerService.GetAllCustomersAsync();
        Customers = new ObservableCollection<Customer>(tempCustomers);
        SelectedCustomer = Customers.FirstOrDefault(x => x.Id == Project.CustomerId)!;
    }

    [RelayCommand]
    private async Task Update()
    {
        //TO DO: sätta dessa värden från drop down lists
        //TO DO: ev. ta bort Total som column i dbtabellen
        
        //Project.CustomerId = SelectedCustomer.Id;
        //Project.CustomerId = 1;
        Project.StatusId = 1;
        Project.UserId = 3;
        Project.ServiceId = 1;
        Project.Total = 10000;

        UpdateProject.Title = Project.Title;
        UpdateProject.Description = Project.Description;
        UpdateProject.StartDate = Project.StartDate;
        UpdateProject.EndDate = Project.EndDate;
        UpdateProject.CustomerId = SelectedCustomer.Id;
        UpdateProject.StatusId = 1;
        UpdateProject.UserId = 3;
        UpdateProject.ServiceId = 1;
        UpdateProject.Total = 10000;

        // Updates an existing Contact to file
        var result = await _projectService.UpdateProjectAsync(Project.Id, UpdateProject);
        if (result != null)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectsViewModel>();
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectsViewModel>();
    }
}
