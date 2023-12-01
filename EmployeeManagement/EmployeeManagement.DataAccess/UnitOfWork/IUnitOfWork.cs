using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
