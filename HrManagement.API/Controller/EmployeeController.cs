using Hrmanage.HRManagement.Application.Service;
using Microsoft.AspNetCore.Mvc;
using Hrmanagement.HRManagement.Domain.Entities;
using hrmana.HRManagement.Application.DTOs;
namespace hrmana.HrManagement.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var employee = await _service.GetAllEmployeeAsync();
                var dto = employee.Select(e => new EmployeeResponseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Phone = e.Phone,
                    Address = e.Address,
                    Description = e.Description,
                    DepartmentName = e.Department.Name,
                    PositionName = e.Position.Name,
                    HiredDate = e.HiredDate
                }).ToList();
                return Ok(dto);
            }
            catch
            {
                return NotFound($"Nothing in DB cuz,crazy");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpId(int id)
        {
            try
            {
                var employee = await _service.GetEmployeeById(id);
                var dto = new EmployeeResponseDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Description = employee.Description,
                    Phone = employee.Phone,
                    DepartmentName = employee.Department.Name,
                    PositionName = employee.Position.Name
                };
                return Ok(dto);
            }
            catch
            {
                return NotFound($"i can't see shit with id {id}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDTO empdto)
        {
            var employee = new EmployeeInfo
            {
                Name = empdto.Name,
                Age = empdto.Age,
                Address = empdto.Address,
                Description = empdto.Description,
                Phone = empdto.Phone,
                DepartmentId = empdto.DepartmentId,
                PositionId = empdto.PositionId
            };
            await _service.AddEmployeeAsync(employee);
            return Ok(employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmp(int id, [FromBody] EmployeeInfo emp)
        {
            if (id != emp.Id) return BadRequest("de fuck cuz,it's doesn't match id ");
            try
            {
                await _service.UpdateEmployeeAsync(emp);
                return Ok(emp);
            }
            catch
            {
                return NotFound("i can't find shit cuz");
            }
        }
        
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteEmployee( int id)
        {
            try
            {
                var employ = await _service.GetEmployeeById(id);
                if(employ != null)
                {
                    await _service.DeleteEmployeeAsync(id);
                    return Ok(id);
                }
                else
                {
                    throw new Exception("i can't find shit cuz,yo bitch ass bitch");
                }
            }
            catch(KeyNotFoundException)
            {
                return NotFound("ain't body shit here cuz");
            }
        }
        
        
    }
}
