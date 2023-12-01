using EmployeeManagement.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace EmployeeManagement.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmployeeManagementDbContext _context;

    public UnitOfWork(EmployeeManagementDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}