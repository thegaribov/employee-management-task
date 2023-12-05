using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee.Response;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Handlers.Employee.Commands;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.Repositories.UnitOfWork;
using FluentAssertions;
using Moq;

namespace EmployeeManagement.Application.UnitTests.Handlers.Employee;

public class DeleteEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    public DeleteEmployeeHandlerTests()
    {
        _employeeRepositoryMock = new();
        _departmentRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapperMock = new();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenEmployeeDoesNotExistAsync()
    {
        var command = new DeleteEmployee.Command
        {
            Id = 1,
        };

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult<Core.Entities.Employee>(null));

        var handler = new DeleteEmployee.Handler(
            _employeeRepositoryMock.Object,
            _unitOfWorkMock.Object);


        await Assert.ThrowsAsync<NotFoundException>(async ()
            => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmployeeDoesNotExistAsync()
    {
        var command = new DeleteEmployee.Command
        {
            Id = 1
        };

        _unitOfWorkMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult<Core.Entities.Employee>(null));

        var handler = new DeleteEmployee.Handler(
            _employeeRepositoryMock.Object,
            _unitOfWorkMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(async ()
            => await handler.Handle(command, default));

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_Success_AllInputsAreOk()
    {
        var command = new DeleteEmployee.Command
        {
            Id = 1
        };

        var expected = new Core.Entities.Employee
        {
            Id = 1,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            MonthlyPayment = 9000,
            DepartmentId = 2,
        };

        _unitOfWorkMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult(new Core.Entities.Employee
            {
                Id = 1,
                Name = "Mahmood",
                Surname = "Garibov",
                Age = 22,
                MonthlyPayment = 9000,
                DepartmentId = 2,
            }));

        _employeeRepositoryMock
            .Setup(r => r.Delete(It.IsAny<Core.Entities.Employee>()));

        var handler = new DeleteEmployee.Handler(
            _employeeRepositoryMock.Object,
            _unitOfWorkMock.Object);

        await handler.Handle(command, default);

        _employeeRepositoryMock.Verify(x =>
            x.Delete(It.Is<Core.Entities.Employee>(e =>
                e.Name == expected.Name &&
                e.Surname == expected.Surname &&
                e.Age == expected.Age &&
                e.BirthDate == expected.BirthDate &&
                e.MonthlyPayment == expected.MonthlyPayment &&
                e.DepartmentId == expected.DepartmentId)),
            Times.Once);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
