using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common;

public abstract class BaseEntity<TKey>
{
    public virtual TKey Id { get; set; }
}
