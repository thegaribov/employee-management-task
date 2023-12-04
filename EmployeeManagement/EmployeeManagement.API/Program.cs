using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Middlewares;
using EmployeeManagement.DataAccess.Persistance;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((ctx, lc)
                => lc.ReadFrom.Configuration(ctx.Configuration));

            ConfigureServices(builder);           

            var app = builder.Build();

            await ApplyMigrations(app.Services);

            ConfigureMiddlewarePipeline(app);

            app.Run();
        }

        
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDatabase(builder.Environment, builder.Configuration);
            builder.Services.AddMediatr();
            builder.Services.AddFluentValidationConfigs();


            builder.Services.AddApiServices();
            builder.Services.RegisterAutoMapper();

            builder.Services.AddApiBehaviorConfigurations();

        }
        private static void ConfigureMiddlewarePipeline(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors(cpb =>
                   cpb.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<PerformanceMiddleware>();

            app.MapControllers();
        }
        private static async Task ApplyMigrations(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                await AutomatedMigration.MigrateAsync(scope.ServiceProvider);
            }
        }

    }
}