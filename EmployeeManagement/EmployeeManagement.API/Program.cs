using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Middlewares;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureMiddlewarePipeline(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDatabase(builder.Environment, builder.Configuration);

            builder.Services.AddApiServices();
            builder.Services.RegisterAutoMapper();

            builder.Services.AddApiBehaviorConfigurations();
        }
        private static void ConfigureMiddlewarePipeline(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseCors(cpb =>
                   cpb.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<PerformanceMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();
        }
    }
}