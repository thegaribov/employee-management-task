using EmployeeManagement.Business.DTOs.Employee.Request;
using EmployeeManagement.Business.Validators.Department;
using FluentValidation.TestHelper;

namespace EmployeeManagement.Application.UnitTests.Validators;

public class UpdateEmployeeRequestDTOValidatorTests
{
    private readonly UpdateEmployeeRequestDTOValidator _validator;

    public UpdateEmployeeRequestDTOValidatorTests()
    {
        _validator = new UpdateEmployeeRequestDTOValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Null()
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { Name = null };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { Name = string.Empty };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }

    [Theory]
    [InlineData("I")]
    [InlineData("Ravan Mahmoodzadeh Ibn Ilham")]
    public void Validator_Should_Have_Error_When_Name_Length_Is_Less_Than_Two_Or_Greater_Than_Twenty(string name)
    {
        // Arrange
        var createDepartmentRequestDto = new UpdateEmployeeRequestDTO { Name = name };

        // Act
        var result = _validator.TestValidate(createDepartmentRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }


    [Fact]
    public void Validator_Should_Have_Error_When_Surname_Is_Null()
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { Surname = null };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Surname);
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Surname_Is_Empty()
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { Surname = string.Empty };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Surname);
    }

    [Theory]
    [InlineData("I")]
    [InlineData("Ravan Mahmoodzadeh Ibn Ilham")]
    public void Validator_Should_Have_Error_When_Surname_Length_Is_Less_Than_Two_Or_Greater_Than_Twenty(string surname)
    {
        // Arrange
        var createDepartmentRequestDto = new UpdateEmployeeRequestDTO { Surname = surname };

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
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { Age = age };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Age);
    }

    [Theory]
    [InlineData(120)]
    [InlineData(150)]
    public void Validator_Should_Have_Error_When_Age_Greater_Than_Hundred(int age)
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { Age = age };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Age);
    }


    [Theory]
    [InlineData(-2)]
    [InlineData(-1)]
    public void Validator_Should_Have_Error_When_MonthlyPayment_Less_Than_Zero(int montlyPayment)
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { MonthlyPayment = montlyPayment };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.MonthlyPayment);
    }

    [Theory]
    [InlineData(10001)]
    [InlineData(12000)]
    public void Validator_Should_Have_Error_When_MonthlyPayment_Greater_Than_Ten_Thousand(int montlyPayment)
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO { MonthlyPayment = montlyPayment };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.MonthlyPayment);
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok()
    {
        // Arrange
        var UpdateEmployeeRequestDTO = new UpdateEmployeeRequestDTO
        {
            Name = "Mahmood",
            Surname = "Garibov",
            Age = 22,
            MonthlyPayment = 9000,
            BirthDate = new DateTime(2001, 01, 06),
            DepartmentId = 1
        };

        // Act
        var result = _validator.TestValidate(UpdateEmployeeRequestDTO);

        // Assert
        result.ShouldNotHaveValidationErrorFor(utl => utl.MonthlyPayment);
    }
}
