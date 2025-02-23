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
    private string headline = "Project List";

    [ObservableProperty]
    private ObservableCollection<Project> _projects = [];

    public ProjectsViewModel(IServiceProvider serviceProvider, IProjectService projectService)
    {
        _serviceProvider = serviceProvider;
        _projectService = projectService;

        //Populates the Project list from the database
        Task.Run(GetProjectsAsync);
    }

    public async Task GetProjectsAsync() 
    {
        var tempProjects = await _projectService.GetAllProjectsAsync();
        Projects = new ObservableCollection<Project>(tempProjects);
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
