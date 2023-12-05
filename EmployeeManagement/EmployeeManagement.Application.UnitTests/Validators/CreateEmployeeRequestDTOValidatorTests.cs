using EmployeeManagement.Business.DTOs.Employee.Request;
using EmployeeManagement.Business.Validators.Department;
using FluentValidation.TestHelper;

namespace EmployeeManagement.Application.UnitTests.Validators;

public class CreateEmployeeRequestDTOValidatorTests
{
    private readonly CreateEmployeeRequestDTOValidator _validator;

    public CreateEmployeeRequestDTOValidatorTests()
    {
        _validator = new CreateEmployeeRequestDTOValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Null()
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { Name = null };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { Name = string.Empty };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }

    [Theory]
    [InlineData("I")]
    [InlineData("Ravan Mahmoodzadeh Ibn Ilham")]
    public void Validator_Should_Have_Error_When_Name_Length_Is_Less_Than_Two_Or_Greater_Than_Twenty(string name)
    {
        // Arrange
        var createDepartmentRequestDto = new CreateEmployeeRequestDTO { Name = name };

        // Act
        var result = _validator.TestValidate(createDepartmentRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }


    [Fact]
    public void Validator_Should_Have_Error_When_Surname_Is_Null()
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { Surname = null };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Surname);
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Surname_Is_Empty()
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { Surname = string.Empty };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Surname);
    }

    [Theory]
    [InlineData("I")]
    [InlineData("Ravan Mahmoodzadeh Ibn Ilham")]
    public void Validator_Should_Have_Error_When_Surname_Length_Is_Less_Than_Two_Or_Greater_Than_Twenty(string surname)
    {
        // Arrange
        var createDepartmentRequestDto = new CreateEmployeeRequestDTO { Surname = surname };

        // Act
        var result = _validator.TestValidate(createDepartmentRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Surname);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validator_Should_Have_Error_When_Age_Less_Than_One(int age)
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { Age = age };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Age);
    }

    [Theory]
    [InlineData(120)]
    [InlineData(150)]
    public void Validator_Should_Have_Error_When_Age_Greater_Than_Hundred(int age)
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { Age = age };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Age);
    }


    [Theory]
    [InlineData(-2)]
    [InlineData(-1)]
    public void Validator_Should_Have_Error_When_MonthlyPayment_Less_Than_Zero(int montlyPayment)
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { MonthlyPayment = montlyPayment };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.MonthlyPayment);
    }

    [Theory]
    [InlineData(10001)]
    [InlineData(12000)]
    public void Validator_Should_Have_Error_When_MonthlyPayment_Greater_Than_Ten_Thousand(int montlyPayment)
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO { MonthlyPayment = montlyPayment };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.MonthlyPayment);
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
    {
        // Arrange
        var createEmployeeRequestDto = new CreateEmployeeRequestDTO
        {
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            MonthlyPayment = 9000,
            BirthDate = new DateTime(2001, 01, 06),
            DepartmentId = 1
        };

        // Act
        var result = _validator.TestValidate(createEmployeeRequestDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(utl => utl.MonthlyPayment);
    }
}
