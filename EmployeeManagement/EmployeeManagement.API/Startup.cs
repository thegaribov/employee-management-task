using EmployeeManagement.API.Extensions;
using EmployeeManagement.API.Middlewares;
using EmployeeManagement.DataAccess.Persistance;
using Serilog;

namespace EmployeeManagement.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDatabase(_env, _configuration);
        services.AddMediatr();
        services.AddFluentValidationConfigs();

        services.AddSerilog();
        services.AddApiServices();
        services.RegisterAutoMapper();

        services.AddApiBehaviorConfigurations();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseSerilogRequestLogging();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseCors(cpb =>
               cpb.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
        );


        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<PerformanceMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
