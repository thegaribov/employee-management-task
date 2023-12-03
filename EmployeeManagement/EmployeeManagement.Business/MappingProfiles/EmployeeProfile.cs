using AutoMapper;
using EmployeeManagement.Business.DTOs.Employee;
using EmployeeManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business.MappingProfiles;

public class EmployeeProfile : Profile
{
	public EmployeeProfile()
	{
		CreateMap<Employee, EmployeeResponseDTO>();

		CreateMap<Employee, EmployeeDetailsResponseDTO>();
    }
}
