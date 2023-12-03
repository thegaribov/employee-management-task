using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.Handlers.Employee.Queries;

public class GetAllEmployees
{
    public class Query : IRequest<IEnumerable<EmployeeResponseDTO>>
    {

    }

    public class Handler : IRequestHandler<Query, IEnumerable<EmployeeResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public Handler(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EmployeeResponseDTO>>(employees);
        }
    }
}
