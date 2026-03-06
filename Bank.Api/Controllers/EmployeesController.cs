using Bank.Application;
using Bank.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll(CancellationToken cancellationToken)
    {
        var employees = await _employeeService.GetAllEmployeesAsync(cancellationToken);
        return Ok(employees);
    }

    [HttpGet("{personalNumber}")]
    public async Task<ActionResult<Employee>> GetByPersonalNumber(string personalNumber, CancellationToken cancellationToken)
    {
        var employee = await _employeeService.GetEmployeeAsync(personalNumber, cancellationToken);
        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }
}

