using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using EmployeeManagement.Repositories.UnitOfWork;
using MediatR;

namespace EmployeeManagement.Business.Handlers.Department.Commands;

public class DeleteDepartment
{
    public class Command : IRequest
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetSingleOrDefaultAsync(e => e.Id == request.Id);
            if (department == null)
            {
                throw new NotFoundException($"Department with id : {request.Id}, not found");
            }

            _departmentRepository.Delete(department);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
