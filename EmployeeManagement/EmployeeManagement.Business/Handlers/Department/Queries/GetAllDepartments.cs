using AutoMapper;
using EmployeeManagement.Business.DTOs.Department.Response;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using MediatR;

namespace EmployeeManagement.Business.Handlers.Department.Queries;

public class GetAllDepartments
{
    public class Query : IRequest<IEnumerable<DepartmentResponseDTO>> { }

    public class Handler : IRequestHandler<Query, IEnumerable<DepartmentResponseDTO>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public Handler(
            IMapper mapper,
            IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentResponseDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DepartmentResponseDTO>>(departments);
        }
    }
}
