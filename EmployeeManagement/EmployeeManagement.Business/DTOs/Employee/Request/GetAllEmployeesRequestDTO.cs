using EmployeeManagement.Core.Helpers.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.DTOs.Employee.Request;

public class GetAllEmployeesRequestDTO : QueryParams
{
    public int? DepartmentId { get; set; }
    public DateTime? BirthDateStart { get; init; }
    public DateTime? BirthDateEnd { get; init; }
}
