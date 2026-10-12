using hrmana.HRManagement.Application.DTOs;
using Hrmanage.HRManagement.Application.Service;
using Hrmanagement.HRManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(Roles = "Admin,Manager,Viewer")]
        public async Task<IActionResult> GetAllEmployee()
        {
           var e =  await _service.GetAllEmployeeAsync();
            return Ok(BaseResponse<List<EmployeeResponseDTO>>.Ok(e, "Get All SuccessFully!"));
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,Viewer")]
        public async Task<IActionResult> GetEmpId(int id)
        {
            var d = await _service.GetEmployeeById(id);
            return Ok(BaseResponse<EmployeeResponseDTO>.Ok(d, $"Get Employee With ID : {id}"));
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
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
                PositionId = empdto.PositionId,
                HiredDate = empdto.HiredDate,
            };
            await _service.AddEmployeeAsync(employee);
            return Ok(employee);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
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
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteEmployee( int id)
        {
            await _service.DeleteEmployeeAsync(id);
           return Ok(BaseResponse<object>.Ok(null,"Delete Successfully"));
        }
        
        
    }
}
