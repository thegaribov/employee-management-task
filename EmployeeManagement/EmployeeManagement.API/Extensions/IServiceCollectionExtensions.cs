using EmployeeManagement.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.API.Extensions
{
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

        //public static IServiceCollection AddMediatr(this IServiceCollection serviceCollection)
        //{
        //    return serviceCollection.AddMediatR(typeof(GetDistanceBetweenAirportsQueryHandler).GetTypeInfo().Assembly);
        //}
    }
}
