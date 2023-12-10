using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Extensions;

public static class DbSetExtensions
{
    public static async Task RemoveAllAsync<T>(this DbSet<T> dbSet) where T : class
    {
        // Load all entities into memory and then remove them
        var allEntities = await dbSet.ToListAsync();
        dbSet.RemoveRange(allEntities);
    }
}
