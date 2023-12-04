using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee.Response;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Business.MappingProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeResponseDTO>();

        CreateMap<Employee, EmployeeDetailsResponseDTO>();
    }
}
