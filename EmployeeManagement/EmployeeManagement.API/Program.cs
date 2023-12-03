using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Middlewares;
using Serilog;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((ctx, lc) 
                => lc.ReadFrom.Configuration(ctx.Configuration));

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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
    }
}