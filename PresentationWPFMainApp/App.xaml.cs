using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationWPFMainApp.ViewModels;
using PresentationWPFMainApp.Views;
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
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\DataStorage_Assignment\Data\Databases\local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
                services.AddScoped<IProjectRepository, ProjectRepository>();
                services.AddScoped<IProjectService, ProjectService>();
                services.AddScoped<ICustomerRepository, CustomerRepository>();
                services.AddScoped<ICustomerService, CustomerService>();

                services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
                services.AddScoped<IStatusTypeService, StatusTypeService>();

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();

                services.AddScoped<IServiceRepository, ServiceRepository>();
                services.AddScoped<IServiceService, ServiceService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ProjectsViewModel>();
                services.AddTransient<ProjectsView>();

                services.AddTransient<AddProjectViewModel>();
                services.AddTransient<AddProjectView>();

                services.AddTransient<DetailsProjectViewModel>();
                services.AddTransient<DetailsProjectView>();

                services.AddTransient<EditProjectViewModel>();
                services.AddTransient<EditProjectView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
