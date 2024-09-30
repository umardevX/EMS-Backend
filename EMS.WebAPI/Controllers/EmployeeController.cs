using EMS.Data.Entities;
using EMS.Services.Services.Interfaces;
using EMS.WebAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) 
        { 
            _employeeService = employeeService;
        }

        [HttpGet("employees")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("employees/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {id} not found.");
            }

            return Ok(employee);
        }

        [HttpPost("employees")]
        [Authorize]
        public async Task<IActionResult> Create(Employee employee)
        {
            // Note: ModelState is handled by FluentValidation, no need for explicit check ModelState

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _employeeService.CreateEmployee(employee);
            return Ok();
        }

        [HttpPut("employees/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employee.EmployeeId = id;
            await _employeeService.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete("employees/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            await _employeeService.DeleteEmployee(id);
            return NoContent();
        }
    }
}
