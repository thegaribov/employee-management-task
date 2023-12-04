using AutoMapper;
using EmployeeManagement.Business.DTOs.Department.Response;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Business.MappingProfiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentResponseDTO>();
    }
}
