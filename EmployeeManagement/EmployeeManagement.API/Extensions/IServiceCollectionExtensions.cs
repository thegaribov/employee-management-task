using EmployeeManagement.Business.MappingProfiles;
using EmployeeManagement.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor();

        //serviceCollection.AddTransient<IGeolocationService, CustomGeolocationService>();
        //serviceCollection.AddTransient<IAirportProvider, CTeleportProvider>();
        return serviceCollection;
    }

    public static IServiceCollection AddDatabase
        (this IServiceCollection serviceCollection, IWebHostEnvironment env, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<EmployeeManagementDbContext>(options =>
        {
            if (env.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), o =>
            {
                o.MigrationsAssembly("EmployeeManagement.DataAccess");
            });
        });

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

    //public static IServiceCollection AddMediatr(this IServiceCollection serviceCollection)
    //{
    //    return serviceCollection.AddMediatR(typeof(GetDistanceBetweenAirportsQueryHandler).GetTypeInfo().Assembly);
    //}
}
