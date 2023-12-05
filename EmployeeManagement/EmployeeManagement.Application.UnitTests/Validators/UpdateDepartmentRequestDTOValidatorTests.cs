using EmployeeManagement.Business.DTOs.Department.Request;
using EmployeeManagement.Business.Validators.Department;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.UnitTests.Validators;

public class UpdateDepartmentRequestDTOValidatorTests
{
    private readonly UpdateDepartmentRequestDTOValidator _validator;

	public UpdateDepartmentRequestDTOValidatorTests()
	{
		_validator = new UpdateDepartmentRequestDTOValidator();
	}

	[Fact]
	public void Validator_Should_Have_Error_When_Name_Is_Null()
	{
        // Arrange
        var updateDepartmentRequestDto = new UpdateDepartmentRequestDTO { Name = null };

        // Act
        var result = _validator.TestValidate(updateDepartmentRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var updateDepartmentRequestDto = new UpdateDepartmentRequestDTO { Name = string.Empty };

        // Act
        var result = _validator.TestValidate(updateDepartmentRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }

    
    [Theory]
    [InlineData("I")]
    [InlineData("Information Technology Department")]
    public void Validator_Should_Have_Error_When_Length_Is_Less_Than_Two_Or_Greater_Than_Ten(string name)
    {
        // Arrange
        var updateDepartmentRequestDto = new UpdateDepartmentRequestDTO { Name = name };

        // Act
        var result = _validator.TestValidate(updateDepartmentRequestDto);

        // Assert
        result.ShouldHaveValidationErrorFor(utl => utl.Name);
    }

    [Theory]
    [InlineData("IT")]
    [InlineData("HR")]
    public void Validator_Should_Not_Have_Error_When_All_Inputs_Are_Ok(string name)
    {
        // Arrange
        var updateDepartmentRequestDto = new UpdateDepartmentRequestDTO { Name = name };

        // Act
        var result = _validator.TestValidate(updateDepartmentRequestDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(utl => utl.Name);
    }
}
