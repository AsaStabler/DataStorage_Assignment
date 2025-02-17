using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresentationConsoleMainApp.Dialogs;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(options => options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"))
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<IServiceRepository, ServiceRepository>()
    .AddScoped<IStatusTypeRepository, StatusTypeRepository>()
    .AddScoped<IUserRepository, UserRepository>()

    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IProjectService, ProjectService>()

    .AddScoped<IMenuDialog, MenuDialog>()
    .BuildServiceProvider();

var customerDialogs = services.GetRequiredService<IMenuDialog>();
await customerDialogs.MenuOptions();

/*
    .AddScoped<IServiceService, ServiceService>()
    .AddScoped<IStatusTypeService, StatusTypeService>()
    .AddScoped<IUserService, UserService>()
*/