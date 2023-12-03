using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using MediatR;

namespace EmployeeManagement.Business.Handlers.Employee.Queries;

public class GetEmployeeById
{
    public class Query : IRequest<IEnumerable<EmployeeDetailsResponseDTO>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<EmployeeDetailsResponseDTO>>
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

        public async Task<IEnumerable<EmployeeDetailsResponseDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository
                .GetSingleOrDefaultAsync(e => e.Id == request.Id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with id : {employee.Id}, not found");
            }

            return _mapper.Map<IEnumerable<EmployeeDetailsResponseDTO>>(employee);
        }
    }
}
