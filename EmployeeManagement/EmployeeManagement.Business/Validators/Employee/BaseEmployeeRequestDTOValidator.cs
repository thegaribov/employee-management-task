using EmployeeManagement.Business.DTOs.Department.Request;
using EmployeeManagement.Business.DTOs.Employee.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Validators.Department;

public class BaseEmployeeRequestDTOValidator<T> : AbstractValidator<T>
    where T : BaseEmployeeRequestDTO
{
    public BaseEmployeeRequestDTOValidator()
    {
        RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .Length(2, 20);

        RuleFor(x => x.Surname)
               .NotNull()
               .NotEmpty()
               .Length(2, 20);

        RuleFor(x => x.Age)
               .GreaterThanOrEqualTo(1)
               .LessThanOrEqualTo(100);

        RuleFor(x => x.MonthlyPayment)
               .GreaterThanOrEqualTo(0)
               .LessThanOrEqualTo(10000);
    }
}