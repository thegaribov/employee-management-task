using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee.Response;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Handlers.Employee.Commands;
using EmployeeManagement.Business.Handlers.Employee.Queries;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.Repositories.UnitOfWork;
using FluentAssertions;
using Moq;

namespace EmployeeManagement.Application.UnitTests.Handlers.Employee;

public class GetEmployeeByIdHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    public GetEmployeeByIdHandlerTests()
    {
        _employeeRepositoryMock = new();
        _departmentRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapperMock = new();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenEmployeeDoesNotExistAsync()
    {
        var command = new GetEmployeeById.Query
        {
            Id = 1,
        };

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult<Core.Entities.Employee>(null));

        var handler = new GetEmployeeById.Handler(
            _employeeRepositoryMock.Object,
            _mapperMock.Object);


        await Assert.ThrowsAsync<NotFoundException>(async ()
            => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_Success_AllInputsAreOk()
    {
        var command = new GetEmployeeById.Query
        {
            Id = 1,
        };

        var expected = new EmployeeResponseDTO
        {
            Id = 1,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            DepartmentId = 1,
        };


        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult<Core.Entities.Employee>(null));

        _mapperMock
            .Setup(r => 
                r.Map<EmployeeDetailsResponseDTO>(It.IsAny<Core.Entities.Employee>()))
            .Returns(() => new EmployeeResponseDTO
            {
                Id = 1,
                Name = "Mahmood",
                Surname = "Garibov",
                Age = 22,
                DepartmentId = 1,
            });

        var handler = new GetEmployeeById.Handler(
            _employeeRepositoryMock.Object,
            _mapperMock.Object);

        var result = await handler.Handle(command, default);

        result.Should().BeEquivalentTo(expected);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
