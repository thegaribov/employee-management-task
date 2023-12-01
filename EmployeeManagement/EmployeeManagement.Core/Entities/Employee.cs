using EmployeeManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Entities;

public class Employee : AuditableEntity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public decimal MonthlyPayment { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public DateTime BirthDate { get; set; }
}
