using EmployeeManagement.Business.DTOs.Employee;
using EmployeeManagement.Business.Handlers.Employee.Commands;
using EmployeeManagement.Business.Handlers.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeResponseDTO>>> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllEmployees.Query()));
    }

    [HttpGet("{id}", Name = "employee-details")]
    public async Task<ActionResult<EmployeeResponseDTO>> GetAsync([FromRoute] int id)
    {
        return Ok(await _mediator.Send(new GetEmployeeById.Query { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeResponseDTO>> Add([FromBody] CreateEmployeeRequestDTO requestDto)
    {
        var employeeDetails = await _mediator.Send(new CreateEmployee.Command
        {
            Name = requestDto.Name,
            Surname = requestDto.Surname,
            Age = requestDto.Age,
            BirthDate = requestDto.BirthDate,
            MonthlyPayment = requestDto.MonthlyPayment,
            DepartmentId = requestDto.DepartmentId,
        });

        var uri = Url.RouteUrl("employee-details", new { Id = employeeDetails.Id });

        return Created(uri, employeeDetails);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeResponseDTO>> Update(
        [FromRoute] int id,
        [FromBody] UpdateEmployeeRequestDTO requestDto)
    {
        var employeeDetails = await _mediator.Send(new UpdateEmployee.Command
        {
            Id = id,
            Name = requestDto.Name,
            Surname = requestDto.Surname,
            Age = requestDto.Age,
            BirthDate = requestDto.BirthDate,
            MonthlyPayment = requestDto.MonthlyPayment,
            DepartmentId = requestDto.DepartmentId,
        });

        return Ok(employeeDetails);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        await _mediator.Send(new DeleteEmployee.Command
        {
            Id = id,
        });

        return NoContent();
    }
}
