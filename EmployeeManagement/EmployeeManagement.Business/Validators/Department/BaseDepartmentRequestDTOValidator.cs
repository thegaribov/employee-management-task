using EmployeeManagement.Business.DTOs.Department.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Validators.Department;

public class DepartmentRequestDTOValidator<T> : AbstractValidator<T>
    where T : BaseDepartmentRequestDTO
{
    public DepartmentRequestDTOValidator()
    {
        RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .Length(2, 10);
    }
}