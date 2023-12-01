using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Contexts;

public class EmployeeManagementDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public EmployeeManagementDbContext(
        DbContextOptions<EmployeeManagementDbContext> options, 
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    //Data protection keys
    //public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }


    #region Auditable logs

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();

        return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        var utcNow = DateTime.UtcNow;
        var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "N/A";

        foreach (var entry in entries)
        {
            if (entry.Entity is not IAuditing auditableEntry) continue;

            if (entry.State == EntityState.Added)
            {
                auditableEntry.CreatedOn = utcNow;
                auditableEntry.CreatedBy = $"UserName:{userName}"; //If we need additional information then we can customuze this line

                auditableEntry.LastModifiedOn = utcNow;
                auditableEntry.LastModifiedBy = $"UserName:{userName}"; //If we need additional information then we can customuze this line
            }
            else if (entry.State == EntityState.Modified)
            {
                auditableEntry.LastModifiedOn = utcNow;
                auditableEntry.LastModifiedBy = $"UserName:{userName}"; //If we need additional information then we can customuze this line
            }
        }
    }

    #endregion
}
