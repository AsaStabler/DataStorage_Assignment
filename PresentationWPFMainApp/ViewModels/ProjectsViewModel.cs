using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace PresentationWPFMainApp.ViewModels;

public partial class ProjectsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProjectService _projectService;

    [ObservableProperty]
    private string title = "Project List";

    [ObservableProperty]
    private ObservableCollection<Project> _projects = [];

    public ProjectsViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;

        //Populates the Project list from the database
        Task.Run(GetProjectsAsync);
        
        //_projects = new ObservableCollection<Project>( await _projectService.GetAllProjectsAsync());
        //_projects = testMethod();
    }

    public async Task GetProjectsAsync() 
    { 
        var tempProjects = await _projectService.GetAllProjectsAsync();
        Projects = new ObservableCollection<Project>(tempProjects);


        //Alt 1
        //IEnumerable<Project> _projectsTest = await _projectService.GetAllProjectsAsync();
        
        //Alt 1a
        //_projectsTest = new ObservableCollection<Project>(_projectsTest);
        //Alt 1a
        //ObservableCollection<Project> _projectsTest2 = new ObservableCollection<Project>(_projectsTest);

        //Alt 2
        //ObservableCollection<Project> _projectsTest3 = [];
        //_projectsTest3 = new ObservableCollection<Project>( await _projectService.GetAllProjectsAsync());

        //return _projectsTest3;
    }

    [RelayCommand]
    private void GoToAddProject()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddProjectViewModel>();
    }

    [RelayCommand]
    private void GoToDetailsProject(Project project)
    {
        var detailsProjectViewModel = _serviceProvider.GetRequiredService<DetailsProjectViewModel>();
        detailsProjectViewModel.Project = project;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = detailsProjectViewModel;
    }
}
