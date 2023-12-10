using EmployeeManagement.Api.IntegrationTests.Constants;
using EmployeeManagement.API;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.DataAccess.Contexts;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagement.Api.IntegrationTests.Fixtures;

public class WebAppFixture
{
    private IHost _host;
    public IHost Host
    {
        get { return _host ??= GetNewHost(); }
    }

    private HttpClient _client;
    public HttpClient Client
    {
        get { return _client ??= GetNewClient(Host); }
    }

    private IHost GetNewHost()
    {
        var hostBuilder = new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                webHost.UseTestServer();
                webHost.UseStartup<Startup>();
                webHost.ConfigureAppConfiguration((_, configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(
                        new Dictionary<string, string>
                        {
                            ["Database:UseInMemoryDatabase"] = "true",
                        });
                }); ;
            });

        var host = hostBuilder.Start();

        SeedMockData(host);


        return host;
    }


    private void SeedMockData(IHost host)
    {
        var context = host.Services.GetRequiredService<EmployeeManagementDbContext>();
        var mockDepartment = Builder<Department>.CreateNew()
                .With(d => d.Id = DepartmentConstants.MOCK_DEPARTMENT_ID)
                .With(d => d.Name = DepartmentConstants.MOCK_DEPARTMENT_NAME)
                .Build();

        var mockEmployee = Builder<Employee>.CreateNew()
                .With(d => d.Id = EmployeeConstants.MOCK_EMPLOYEE_ID)
                .With(d => d.Name = EmployeeConstants.MOCK_EMPLOYEE_NAME)
                .With(d => d.Surname = EmployeeConstants.MOCK_EMPLOYEE_SURNAME)
                .With(d => d.Age = EmployeeConstants.MOCK_EMPLOYEE_AGE)
                .With(d => d.MonthlyPayment = EmployeeConstants.MOCK_EMPLOYEE_MONTHLY_PAYMENT)
                .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
                .Build();

        context.Departments.Add(mockDepartment);
        context.SaveChanges();
    }

    private HttpClient GetNewClient(IHost host)
    {
        return host.GetTestClient();
    }
}
