using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Helpers.Paging;
using EmployeeManagement.Repositories.Abstracts;

namespace EmployeeManagement.DataAccess.Repositories.Abstracts;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<Paginator<Employee>> GetAllPaginatedFilteredSorted(QueryParams queryParams, int? departmentId);
}
