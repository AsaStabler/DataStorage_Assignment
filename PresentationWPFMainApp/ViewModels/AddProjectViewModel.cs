using Business.DTOs;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace PresentationWPFMainApp.ViewModels;

public partial class AddProjectViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;
    private readonly ICustomerService _customerService;
    private readonly IStatusTypeService _statusTypeService;
    private readonly IUserService _userService;
    private readonly IServiceService _serviceService;

    [ObservableProperty]
    private string headline = "New Project";

    //The ProjectRegistrationForm to be filled with input data in AddProjectView
    [ObservableProperty]
    private ProjectRegistrationForm _project = new(); 

    //To populate the Customers ComboBox
    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    //To get the selected Customer from the Customers Combobox
    [ObservableProperty]
    private Customer _selectedCustomer = new();

    //To populate the StatusTypes ComboBox
    [ObservableProperty]
    private ObservableCollection<StatusType> _statusTypes = [];

    //To get the selected StatusType from the StatusTypes Combobox
    [ObservableProperty]
    private StatusType _selectedStatusType = new();

    //To populate the Users ComboBox
    [ObservableProperty]
    private ObservableCollection<User> _users = [];

    //To get the selected User from the Users Combobox
    [ObservableProperty]
    private User _selectedUser = new();

    //To populate the Services ComboBox
    [ObservableProperty]
    private ObservableCollection<Service> _services = [];

    //To get the selected Service from the Services Combobox
    [ObservableProperty]
    private Service _selectedService = new();

    //To display an error message
    [ObservableProperty]
    private string _errorMsg = null!;

    public AddProjectViewModel(IServiceProvider serviceProvider, IProjectService projectService, 
                               ICustomerService customerService, IStatusTypeService statusTypeService,
                               IUserService userService, IServiceService serviceService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;
        _customerService = customerService;
        _statusTypeService = statusTypeService;
        _userService = userService;
        _serviceService = serviceService;

        //Populates the lists of Customers, StatusTypes, Users and Services
        //from the database; to be used to populate drop down lists
        Task.Run(GetListsForComboBoxesAsync);
    }

    public async Task GetListsForComboBoxesAsync()
    {
        var tempCustomers = await _customerService.GetAllCustomersAsync();
        Customers = new ObservableCollection<Customer>(tempCustomers);

        var tempStatusTypes = await _statusTypeService.GetAllStatusTypesAsync();
        StatusTypes = new ObservableCollection<StatusType>(tempStatusTypes);

        var tempUsers = await _userService.GetAllUsersAsync();
        Users = new ObservableCollection<User>(tempUsers);

        var tempServices = await _serviceService.GetAllServicesAsync();
        Services = new ObservableCollection<Service>(tempServices);
    }

    [RelayCommand]
    private async Task Save()
    {
        Project.CustomerId = SelectedCustomer.Id;
        Project.StatusId = SelectedStatusType.Id;
        Project.UserId = SelectedUser.Id;
        Project.ServiceId = SelectedService.Id;

        // Saves a new Project to the database
        var result = await _projectService.CreateProjectAsync(Project);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectsViewModel>();
        }
        else
        {
            ErrorMsg = "The project could not be saved.";
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectsViewModel>();
    }
}