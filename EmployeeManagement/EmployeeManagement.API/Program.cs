using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Middlewares;
using EmployeeManagement.DataAccess.Persistance;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                await AutomatedMigration.MigrateAsync(scope.ServiceProvider);
            }

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((_, configBuilder) =>
                    {
                        configBuilder.AddEnvironmentVariables();
                    }); ;
                });
    }
}