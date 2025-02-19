using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;

namespace PresentationWPFMainApp.ViewModels;

public partial class DetailsProjectViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private string headline = "Project Details";  

    [ObservableProperty]
    private Project _project = new();   //The Project to be seen in DetailsProjectView

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    [ObservableProperty]
    private Customer _selectedCustomer = new();

    public DetailsProjectViewModel(IServiceProvider serviceProvider, IProjectService projectService, ICustomerService customerService)
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
    private void GoToEditProject()
    {
        var editProjectViewModel = _serviceProvider.GetRequiredService<EditProjectViewModel>();
        editProjectViewModel.Project = Project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = editProjectViewModel;
    }
   
    [RelayCommand]
    private async Task Delete()
    {
        // Delete an existing Projcet from the database
        var result =  await _projectService.DeleteProjectByIdAsync(Project.Id);
        if (result)
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
