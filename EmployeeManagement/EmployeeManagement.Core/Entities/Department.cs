using EmployeeManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Entities;

public class Department : AuditableEntity<int>
{
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; set; }
}
