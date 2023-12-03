using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.DTOs.Employee;

public class CreateEmployeeRequestDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public decimal MonthlyPayment { get; set; }
    public DateTime BirthDate { get; set; }

    public int DepartmentId { get; set; }
}
