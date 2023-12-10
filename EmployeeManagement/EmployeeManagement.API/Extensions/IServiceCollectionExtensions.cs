using EmployeeManagement.API.Options;
using EmployeeManagement.Business.Handlers.Employee.Queries;
using EmployeeManagement.Business.MappingProfiles;
using EmployeeManagement.Business.Validators.Department;
using EmployeeManagement.DataAccess.Contexts;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.DataAccess.Repositories.Implementations;
using EmployeeManagement.Repositories.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmployeeManagement.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddDatabase
        (this IServiceCollection serviceCollection, IWebHostEnvironment env, IConfiguration configuration)
    {
        var databaseConfig = configuration.GetSection("Database").Get<DatabaseConfigOptions>();

        if (databaseConfig.UseInMemoryDatabase)
        {
            serviceCollection.AddDbContext<EmployeeManagementDbContext>(options =>
            {
                options.UseInMemoryDatabase("EmployeeManagement");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
        else
        {
            serviceCollection.AddDbContext<EmployeeManagementDbContext>(options =>
            {
                if (env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }

                options.UseSqlServer(databaseConfig.ConnectionString, o =>
                {
                    o.MigrationsAssembly("EmployeeManagement.DataAccess");
                });
            });
        }

        return serviceCollection;
    }


    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DepartmentProfile).Assembly);
    }

    public static void AddApiBehaviorConfigurations(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(o =>
        {
            o.InvalidModelStateResponseFactory = actionContext =>
               new BadRequestObjectResult(new { Errors = actionContext.ModelState.SerializeErrors() });
        });
    }

    public static IServiceCollection AddMediatr(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(GetAllEmployees).Assembly));
    }

    public static IServiceCollection AddFluentValidationConfigs(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateEmployeeRequestDTOValidator>();
    }
}
