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
    private readonly IStatusTypeService _statusTypeService;
    private readonly IUserService _userService;
    private readonly IServiceService _serviceService;

    [ObservableProperty]
    private string headline = "Edit Project";

    //The original Project to be edited in EditProjectView
    [ObservableProperty]
    private Project _project = new();

    //The ProjectUpdateForm with updated values from EditProjectForm
    [ObservableProperty] 
    private ProjectUpdateForm _updateProject = new();

    //To populate the Customers ComboBox
    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    //To set/get the selected Customer in/from the Customers ComboBox
    [ObservableProperty]
    private Customer _selectedCustomer = new();

    //To populate the StatusTypes ComboBox
    [ObservableProperty]
    private ObservableCollection<StatusType> _statusTypes = [];

    //To set/get the selected StatusType in/from the StatusTypes Combobox
    [ObservableProperty]
    private StatusType _selectedStatusType = new();

    //To populate the Users ComboBox
    [ObservableProperty]
    private ObservableCollection<User> _users = [];

    //To set/get the selected User in/from the Users Combobox
    [ObservableProperty]
    private User _selectedUser = new();

    //To populate the Services ComboBox
    [ObservableProperty]
    private ObservableCollection<Service> _services = [];

    //To set/get the selected Service in/from the Services Combobox
    [ObservableProperty]
    private Service _selectedService = new();

    //To display an error message
    [ObservableProperty]
    private string _errorMsg = null!;

    public EditProjectViewModel(IServiceProvider serviceProvider, IProjectService projectService,
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
        //Also sets the current Projects values - to be shown in ComboBoxes
        Task.Run(GetListsForComboBoxesAsync);
    }

    public async Task GetListsForComboBoxesAsync()
    {
        var tempCustomers = await _customerService.GetAllCustomersAsync();
        Customers = new ObservableCollection<Customer>(tempCustomers);
        SelectedCustomer = Customers.FirstOrDefault(x => x.Id == Project.CustomerId)!;

        var tempStatusTypes = await _statusTypeService.GetAllStatusTypesAsync();
        StatusTypes = new ObservableCollection<StatusType>(tempStatusTypes);
        SelectedStatusType = StatusTypes.FirstOrDefault(x => x.Id == Project.StatusId)!;

        var tempUsers = await _userService.GetAllUsersAsync();
        Users = new ObservableCollection<User>(tempUsers);
        SelectedUser = Users.FirstOrDefault(x => x.Id == Project.UserId)!;

        var tempServices = await _serviceService.GetAllServicesAsync();
        Services = new ObservableCollection<Service>(tempServices);
        SelectedService = Services.FirstOrDefault(x => x.Id == Project.ServiceId)!;
    }

    [RelayCommand]
    private async Task Update()
    {
        UpdateProject.Title = Project.Title;
        UpdateProject.Description = Project.Description;
        UpdateProject.StartDate = Project.StartDate;
        UpdateProject.EndDate = Project.EndDate;
        UpdateProject.CustomerId = SelectedCustomer.Id;
        UpdateProject.StatusId = SelectedStatusType.Id;
        UpdateProject.UserId = SelectedUser.Id;
        UpdateProject.ServiceId = SelectedService.Id;

        // Updates an existing Project in the database
        var result = await _projectService.UpdateProjectAsync(Project.Id, UpdateProject);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectsViewModel>();
        }
        else
        {
            ErrorMsg = "The project could not be updated.";
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProjectsViewModel>();
    }
}
