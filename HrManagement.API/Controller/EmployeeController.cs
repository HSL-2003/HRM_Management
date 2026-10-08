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
           var e =  await _service.GetAllEmployeeAsync();
            return Ok(BaseResponse<List<EmployeeResponseDTO>>.Ok(e, "Get All SuccessFully!"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpId(int id)
        {
            var d = await _service.GetEmployeeById(id);
            return Ok(BaseResponse<EmployeeResponseDTO>.Ok(d, $"Get Employee With ID : {id}"));
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
