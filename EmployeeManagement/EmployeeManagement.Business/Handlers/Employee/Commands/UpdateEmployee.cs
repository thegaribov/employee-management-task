using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee.Response;
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

public class UpdateEmployee
{
    public class Command : IRequest<EmployeeResponseDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public decimal MonthlyPayment { get; set; }
        public DateTime BirthDate { get; set; }

        public int DepartmentId { get; set; }
    }

    public class Handler : IRequestHandler<Command, EmployeeResponseDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeResponseDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetSingleOrDefaultByIdAsync(request.Id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with id : {request.Id}, not found");
            }

            var department = await _departmentRepository.GetSingleOrDefaultByIdAsync(request.DepartmentId);
            if (department == null)
            {
                throw new NotFoundException($"Deparment with id : {request.DepartmentId}, not found.");
            }

            employee.Name = request.Name;
            employee.Surname = request.Surname;
            employee.Age = request.Age;
            employee.MonthlyPayment = request.MonthlyPayment;
            employee.BirthDate = request.BirthDate;
            employee.DepartmentId = department.Id;

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeResponseDTO>(employee);
        }
    }
}
