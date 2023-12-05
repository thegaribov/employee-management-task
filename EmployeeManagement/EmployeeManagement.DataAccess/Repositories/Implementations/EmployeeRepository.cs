using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Extensions;
using EmployeeManagement.Core.Helpers.Paging;
using EmployeeManagement.DataAccess.Contexts;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.DataAccess.Repositories.Implementations;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(
        EmployeeManagementDbContext dbContext,
        ILogger<BaseRepository<Employee>> logger)
        : base(dbContext, logger) { }


    public async Task<Paginator<Employee>> GetAllPaginatedFilteredSorted(QueryParams queryParams, int? departmentId)
    {
        var querySet = _context.Employees.AsQueryable();

        querySet = GetDepartmentQuery(querySet, departmentId);
        querySet = GetSearchQuery(querySet, queryParams.Search);
        querySet = GetSortQuery(querySet, queryParams.Sort);

        var paginator = new Paginator<Employee>(querySet, queryParams.Page, queryParams.PageSize);
        paginator.Records = await paginator.QuerySet.ToListAsync();

        return paginator;

        IQueryable<Employee> GetDepartmentQuery(IQueryable<Employee> querySet, int? departmentId)
        {
            return querySet.WhereIf(departmentId is not null, e => e.DepartmentId == departmentId);
        }
        IQueryable<Employee> GetSortQuery(IQueryable<Employee> querySet, string sortQuery)
        {
            switch (sortQuery?.ToLowerInvariant())
            {
                case "name_asc":
                    return querySet.OrderBy(x => x.Name);
                case "name_desc":
                    return querySet.OrderByDescending(x => x.Name);

                case "surname_asc":
                    return querySet.OrderBy(x => x.Surname);
                case "surname_desc":
                    return querySet.OrderByDescending(x => x.Surname);

                case "age_asc":
                    return querySet.OrderBy(x => x.Age);
                case "age_desc":
                    return querySet.OrderByDescending(x => x.Age);

                case "monthlypayment_asc":
                    return querySet.OrderBy(x => x.MonthlyPayment);
                case "monthlypayment_desc":
                    return querySet.OrderByDescending(x => x.MonthlyPayment);

                case "createdon_asc":
                    return querySet.OrderBy(x => x.CreatedOn);
                case "createdon_desc":
                    return querySet.OrderByDescending(x => x.CreatedOn);

                default:
                    return querySet.OrderByDescending(x => x.CreatedOn);
            }
        }
        IQueryable<Employee> GetSearchQuery(IQueryable<Employee> querySet, string searchQuery)
        {
            return querySet.WhereIf(!string.IsNullOrEmpty(searchQuery), e =>
                EF.Functions.Like(e.Name, $"%{searchQuery}%"));
        }
    }

    public async Task<Employee> GetSingleOrDefaultByIdAsync(int id)
    {
        return await _context.Employees.SingleOrDefaultAsync(d => d.Id == id);
    }
}
