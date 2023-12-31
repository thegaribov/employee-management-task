﻿using EmployeeManagement.Core.Entities;
using EmployeeManagement.DataAccess.Contexts;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repositories.Implementations;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(
        EmployeeManagementDbContext dbContext,
        ILogger<BaseRepository<Department>> logger)
        : base(dbContext, logger) { }

    public async Task<Department> GetSingleOrDefaultByIdAsync(int id)
    {
        return await _context.Departments.SingleOrDefaultAsync(d => d.Id == id);
    } 
}
