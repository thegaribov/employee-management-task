using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee.Response;
using EmployeeManagement.Core.Helpers.Paging;
using EmployeeManagement.DataAccess.Repositories.Abstracts;
using Marketplace.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Business.Handlers.Employee.Queries;

public class GetAllEmployees
{
    public class Query : QueryParams, IRequest<IEnumerable<EmployeeResponseDTO>>
    {
        public int? DepartmentId { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<EmployeeResponseDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var employeesPaginator = await _employeeRepository
                .GetAllPaginatedFilteredSorted(request, request.DepartmentId);

            _httpContextAccessor.HttpContext.Response.Headers
                .Add(CustomHeaderNames.XPagination, employeesPaginator.ToString());

            return _mapper.Map<IEnumerable<EmployeeResponseDTO>>(employeesPaginator.Records);
        }
    }
}
