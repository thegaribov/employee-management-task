using EmployeeManagement.Business.DTOs.Department.Request;
using EmployeeManagement.Business.DTOs.Department.Response;
using EmployeeManagement.Business.DTOs.Employee;
using EmployeeManagement.Business.Handlers.Department.Commands;
using EmployeeManagement.Business.Handlers.Department.Queries;
using EmployeeManagement.Business.Handlers.Employee.Commands;
using EmployeeManagement.Business.Handlers.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[Route("api/departments")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentResponseDTO>>> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllDepartments.Query()));
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentResponseDTO>> Add(
        [FromBody] CreateDepartmentRequestDTO requestDto)
    {
        var departmentDetails = await _mediator.Send(new CreateDepartment.Command
        {
            Name = requestDto.Name,
        });

        return Created(string.Empty, departmentDetails);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DepartmentResponseDTO>> Update(
        [FromRoute] int id,
        [FromBody] UpdateDepartmentRequestDTO requestDto)
    {
        var departmentDetails = await _mediator.Send(new UpdateDepartment.Command
        {
            Id = id,
            Name = requestDto.Name,
        });

        return Ok(departmentDetails);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        await _mediator.Send(new DeleteDepartment.Command
        {
            Id = id,
        });

        return NoContent();
    }
}
