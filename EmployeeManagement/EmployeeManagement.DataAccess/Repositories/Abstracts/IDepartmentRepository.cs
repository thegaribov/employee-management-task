using EmployeeManagement.Core.Entities;
using EmployeeManagement.Repositories.Abstracts;

namespace EmployeeManagement.DataAccess.Repositories.Abstracts;

public interface IDepartmentRepository : IBaseRepository<Department>
{
    Task<Department> GetSingleOrDefaultByIdAsync(int id);
}
