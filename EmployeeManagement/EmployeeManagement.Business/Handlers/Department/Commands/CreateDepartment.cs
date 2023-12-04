using AutoMapper;
using EmployeeManagement.Business.DTOs.Department.Response;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.Repositories.UnitOfWork;
using MediatR;

namespace EmployeeManagement.Business.Handlers.Department.Commands;

public class CreateDepartment
{
    public class Command : IRequest<DepartmentResponseDTO>
    {
        public string Name { get; set; }
    }

    public class Handler : IRequestHandler<Command, DepartmentResponseDTO>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DepartmentResponseDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var department = new Core.Entities.Department
            {
                Name = request.Name,
            };

            _departmentRepository.Add(department);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DepartmentResponseDTO>(department);
        }
    }
}
