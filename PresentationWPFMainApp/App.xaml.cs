using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationWPFMainApp.ViewModels;
using PresentationWPFMainApp.Views;

using System.Configuration;
using System.Data;
using System.Windows;

namespace PresentationWPFMainApp;

public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                //services.AddSingleton<IFileService>(new FileService("Data", "contactlist.json"));
                services.AddSingleton<IProjectRepository, ProjectRepository>();
                services.AddSingleton<IProjectService, ProjectService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                //services.AddTransient<GetStartedViewModel>();
                //services.AddTransient<GetStartedView>();

                services.AddTransient<ProjectsViewModel>();
                services.AddTransient<ProjectsView>();

                services.AddTransient<AddProjectViewModel>();
                services.AddTransient<AddProjectView>();

                //services.AddTransient<DetailsContactViewModel>();
                //services.AddTransient<DetailsContactView>();

                //services.AddTransient<EditContactViewModel>();
                //services.AddTransient<EditContactView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
