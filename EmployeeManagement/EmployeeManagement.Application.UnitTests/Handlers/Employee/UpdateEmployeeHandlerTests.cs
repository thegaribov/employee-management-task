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

public class UpdateEmployeeHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    public UpdateEmployeeHandlerTests()
    {
        _employeeRepositoryMock = new();
        _departmentRepositoryMock = new();
        _unitOfWorkMock = new();
        _mapperMock = new();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenEmployeeDoesNotExistAsync()
    {
        var command = new UpdateEmployee.Command
        {
            Id = 2,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            BirthDate = new DateTime(2001, 01, 06),
            MonthlyPayment = 9000,
            DepartmentId = 1
        };

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 2)))
            .Returns(Task.FromResult<Core.Entities.Employee>(null));

        var handler = new UpdateEmployee.Handler(
            _employeeRepositoryMock.Object,
            _departmentRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(async ()
            => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmployeeDoesNotExistAsync()
    {
        var command = new UpdateEmployee.Command
        {
            Id = 2,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            BirthDate = new DateTime(2001, 01, 06),
            MonthlyPayment = 9000,
            DepartmentId = 1
        };

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 2)))
            .Returns(Task.FromResult<Core.Entities.Employee>(null));

        var handler = new UpdateEmployee.Handler(
            _employeeRepositoryMock.Object,
            _departmentRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(async ()
            => await handler.Handle(command, default));

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
    }


    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenDepartmentDoesNotExistAsync()
    {
        var command = new UpdateEmployee.Command
        {
            Id = 2,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            BirthDate = new DateTime(2001, 01, 06),
            MonthlyPayment = 9000,
            DepartmentId = 1
        };

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 2)))
            .Returns(Task.FromResult<Core.Entities.Employee>(new Core.Entities.Employee { Id = 2 }));

        _departmentRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult<Core.Entities.Department>(null));


        var handler = new UpdateEmployee.Handler(
            _employeeRepositoryMock.Object,
            _departmentRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(async ()
            => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenDepartmentDoesNotExistAsync()
    {
        var command = new UpdateEmployee.Command
        {
            Id = 2,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            BirthDate = new DateTime(2001, 01, 06),
            MonthlyPayment = 9000,
            DepartmentId = 1
        };

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 2)))
            .Returns(Task.FromResult<Core.Entities.Employee>(new Core.Entities.Employee { Id = 2 }));

        _departmentRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult<Core.Entities.Department>(null));


        var handler = new UpdateEmployee.Handler(
            _employeeRepositoryMock.Object,
            _departmentRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(async ()
            => await handler.Handle(command, default));

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
    }



    [Fact]
    public async Task Handle_Should_Success_AllInputsAreOk()
    {
        var command = new UpdateEmployee.Command
        {
            Id = 2,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            BirthDate = new DateTime(2001, 01, 06),
            MonthlyPayment = 9000,
            DepartmentId = 1
        };

        var expected = new EmployeeResponseDTO
        {
            Id = 2,
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            DepartmentId = 1,
        };

        _employeeRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 2)))
            .Returns(Task.FromResult(new Core.Entities.Employee { Id = 2 }));

        _departmentRepositoryMock
            .Setup(r =>
                r.GetSingleOrDefaultByIdAsync(It.Is<int>(i => i == 1)))
            .Returns(Task.FromResult(new Core.Entities.Department { Id = 1 }));

        _unitOfWorkMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        _mapperMock
             .Setup(r => r.Map<EmployeeResponseDTO>(It.IsAny<Core.Entities.Employee>()))
             .Returns(() => new EmployeeResponseDTO
             {
                 Id = 2,
                 Name = "Mahmood",
                 Surname = "Garibov",
                 Age = 22,
                 DepartmentId = 1,
             });

        var handler = new UpdateEmployee.Handler(
            _employeeRepositoryMock.Object,
            _departmentRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);

        var result = await handler.Handle(command, default);

        result.Should().BeEquivalentTo(expected);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);

        _mapperMock.Verify(x =>
            x.Map<EmployeeResponseDTO>(It.Is<Core.Entities.Employee>(e =>
                 e.Name == command.Name &&
                 e.Surname == command.Surname &&
                 e.Age == command.Age &&
                 e.BirthDate == command.BirthDate &&
                 e.MonthlyPayment == command.MonthlyPayment &&
                 e.DepartmentId == command.DepartmentId)), Times.Once);
    }
}
