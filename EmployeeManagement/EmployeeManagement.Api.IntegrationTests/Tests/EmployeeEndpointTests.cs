using EmployeeManagement.Api.IntegrationTests.Constants;
using EmployeeManagement.Api.IntegrationTests.Fixtures;
using EmployeeManagement.Api.IntegrationTests.Helpers;
using EmployeeManagement.Business.DTOs.Employee.Request;
using EmployeeManagement.Business.DTOs.Employee.Response;
using EmployeeManagement.Business.Validators.Department;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Extensions;
using EmployeeManagement.DataAccess.Contexts;
using EmployeeManagement.DataAccess.Extensions;
using FizzWare.NBuilder;
using FluentAssertions;
using Marketplace.Common.Constants;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace EmployeeManagement.Api.IntegrationTests.Tests;

public class EmployeeEndpointTests : IClassFixture<WebAppFixture>
{
    private IHost _host;
    private HttpClient _client;

    public EmployeeEndpointTests(WebAppFixture webAppFixture)
    {
        _host = webAppFixture.Host;
        _client = webAppFixture.Client;
    }

    [Fact]
    public async Task Add_Should_Create_Employee_In_Database_Successfully()
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var createRequestDto = Builder<CreateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PostAsync("/api/employees", new Helpers.JsonContent(createRequestDto));
        var responseDto = await httpResponse.Content.ReadFromJsonAsync<EmployeeResponseDTO>();
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Id == responseDto.Id);


        Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
 
        employeeFromDatabase.Should().NotBeNull();
        employeeFromDatabase.Should().BeEquivalentTo(createRequestDto);
    }

    [Fact]
    public async Task Add_Should_Return_NotFound_When_DepartmentNotFound_In_Database()
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();
        await context.Employees.RemoveAllAsync();
        await context.SaveChangesAsync();

        var createRequestDto = Builder<CreateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.DepartmentId = 2)
            .Build();

        var httpResponse = await _client.PostAsync("/api/employees", new Helpers.JsonContent(createRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Name == createRequestDto.Name);


        Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        employeeFromDatabase.Should().BeNull();
    }

    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("M")]
    [InlineData("Ravannnnnnnnnnnnnnnnn")]
    public async Task Add_Should_Return_BadRequest_When_Name_Is_Incorrent(string name)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var createRequestDto = Builder<CreateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.Name = name)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PostAsync("/api/employees", new Helpers.JsonContent(createRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Name == createRequestDto.Name);

        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        employeeFromDatabase.Should().BeNull();
    }


    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("M")]
    [InlineData("Ravannnnnnnnnnnnnnnnn")]
    public async Task Add_Should_Return_BadRequest_When_Surname_Is_Incorrent(string surname)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var createRequestDto = Builder<CreateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.Surname = surname)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PostAsync("/api/employees", new Helpers.JsonContent(createRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Name == createRequestDto.Name);


        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        employeeFromDatabase.Should().BeNull();
    }

    [Theory]
    [InlineData(120)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Add_Should_Return_BadRequest_When_Age_Is_Incorrent(int age)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();
        await context.Employees.RemoveAllAsync();
        await context.SaveChangesAsync();

        var createRequestDto = Builder<CreateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.Age = age)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PostAsync("/api/employees", new Helpers.JsonContent(createRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Name == createRequestDto.Name);


        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        employeeFromDatabase.Should().BeNull();
    }

    [Theory]
    [InlineData(20000)]
    [InlineData(-1)]
    public async Task Add_Should_Return_BadRequest_When_MontylyPayment_Is_Incorrent(decimal monthlyPayment)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var createRequestDto = Builder<CreateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.MonthlyPayment = monthlyPayment)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PostAsync("/api/employees", new Helpers.JsonContent(createRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Name == createRequestDto.Name);


        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        employeeFromDatabase.Should().BeNull();
    }

    [Fact]
    public async Task Update_Should_Update_Employee_In_Database_Successfully()
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();


        var updateRequestDto = Builder<UpdateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .With(d => d.Name = "Elchin")
            .Build();

        //act
        var httpResponse = await _client.PutAsync($"/api/employees/{employee.Id}", new Helpers.JsonContent(updateRequestDto));
        var responseDto = await httpResponse.Content.ReadFromJsonAsync<EmployeeResponseDTO>();

        await context.Entry(employee).ReloadAsync();
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Id == responseDto.Id);


        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        employeeFromDatabase.Should().NotBeNull();
        employeeFromDatabase.Should().BeEquivalentTo(updateRequestDto);
    }

    [Fact]
    public async Task Update_Should_Return_NotFound_When_Employee_DoesNot_Exist_In_Database()
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();
        await context.Employees.RemoveAllAsync();
        await context.SaveChangesAsync();

        var updateRequestDto = Builder<UpdateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .With(d => d.Name = "Elchin")
            .Build();

        //act
        var httpResponse = await _client.PutAsync($"/api/employees/4", new Helpers.JsonContent(updateRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Name == updateRequestDto.Name);

        Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        employeeFromDatabase.Should().BeNull();
    }

    [Fact]
    public async Task Update_Should_Return_NotFound_When_Department_DoesNot_Exist_In_Database()
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();
        await context.Employees.RemoveAllAsync();
        await context.SaveChangesAsync();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();

        var updateRequestDto = Builder<UpdateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.DepartmentId = 1)
            .With(d => d.Name = "Elchin")
            .Build();

        //act
        var httpResponse = await _client.PutAsync($"/api/employees/{employee.Id}", new Helpers.JsonContent(updateRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Name == updateRequestDto.Name);

        Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        employeeFromDatabase.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("M")]
    [InlineData("Ravannnnnnnnnnnnnnnnn")]
    public async Task Update_Should_Return_BadRequest_When_Name_Is_Incorrent(string name)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();


        var updateRequestDto = Builder<UpdateEmployeeRequestDTO>
            .CreateNew()
            .With(d => d.Name = name)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PutAsync($"/api/employees/{employee.Id}", new Helpers.JsonContent(updateRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Id == employee.Id);


        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        Assert.NotEqual(employeeFromDatabase.Name, updateRequestDto.Name);
    }


    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("M")]
    [InlineData("Ravannnnnnnnnnnnnnnnn")]
    public async Task Update_Should_Return_BadRequest_When_Surname_Is_Incorrent(string surname)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();


        var updateRequestDto = Builder<UpdateEmployeeRequestDTO>
        .CreateNew()
            .With(d => d.Surname = surname)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PutAsync($"/api/employees/{employee.Id}", new Helpers.JsonContent(updateRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Id == employee.Id);


        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        Assert.NotEqual(employeeFromDatabase.Surname, updateRequestDto.Surname);
    }

    [Theory]
    [InlineData(120)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Update_Should_Return_BadRequest_When_Age_Is_Incorrent(int age)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();


        var updateRequestDto = Builder<UpdateEmployeeRequestDTO>
        .CreateNew()
            .With(d => d.Age = age)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PutAsync($"/api/employees/{employee.Id}", new Helpers.JsonContent(updateRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Id == employee.Id);


        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        Assert.NotEqual(employeeFromDatabase.Age, updateRequestDto.Age);
    }

    [Theory]
    [InlineData(20000)]
    [InlineData(-1)]
    public async Task Update_Should_Return_BadRequest_When_MontylyPayment_Is_Incorrent(decimal monthlyPayment)
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();


        var updateRequestDto = Builder<UpdateEmployeeRequestDTO>
        .CreateNew()
            .With(d => d.MonthlyPayment = monthlyPayment)
            .With(d => d.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        var httpResponse = await _client.PutAsync($"/api/employees/{employee.Id}", new Helpers.JsonContent(updateRequestDto));
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Id == employee.Id);


        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        Assert.NotEqual(employeeFromDatabase.MonthlyPayment, updateRequestDto.MonthlyPayment);
    }

    [Fact]
    public async Task Delete_Should_Delete_Employee_In_Database_Successfully()
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();

        var httpResponse = await _client.DeleteAsync($"/api/employees/{employee.Id}");
        var employeeFromDatabase = await context.Employees.SingleOrDefaultAsync(e => e.Id == employee.Id);

        Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);

        employeeFromDatabase.Should().BeNull();
    }

    [Fact]
    public async Task Delete_Should_Return_NotFound_When_Employee_Does_Not_Exist_In_Database()
    {
        //arrange
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();

        var employee = new Employee
        {
            Name = "test name",
            Surname = "test surname",
            Age = 23,
            MonthlyPayment = 2,
            DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID
        };

        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();

        var httpResponse = await _client.DeleteAsync($"/api/employees/-2");

        Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
    }

    [Fact]
    public async Task Get_Employees_Should_Return_Employees_With_DefaultPageSize_From_Database()
    {
        //arrange 
        const int defaultPageSize = 10;
        var context = _host.Services.GetRequiredService<EmployeeManagementDbContext>();
        var employees = Builder<Employee>
            .CreateListOfSize(20)
            .All()
            .With(e => e.DepartmentId = DepartmentConstants.MOCK_DEPARTMENT_ID)
            .Build();

        await context.Employees.AddRangeAsync(employees);
        await context.SaveChangesAsync();

        var httpResponse = await _client.GetAsync("/api/employees");
        var employeeDtos = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<EmployeeResponseDTO>>();

        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        employeeDtos.Should().HaveCount(defaultPageSize);
        httpResponse.Headers.Should().Contain(x => x.Key == CustomHeaderNames.XPagination);
    }
}
