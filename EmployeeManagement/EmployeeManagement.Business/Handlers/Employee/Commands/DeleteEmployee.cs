using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.Repositories.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Handlers.Employee.Commands;

public class DeleteEmployee
{
    public class Command : IRequest
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetSingleOrDefaultAsync(e => e.Id == request.Id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with id : {employee.Id}, not found");
            }

            _employeeRepository.Delete(employee);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
