using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common;

public class AuditableEntity<T> : BaseEntity<T>, IAuditing
{
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}
