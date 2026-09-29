using hrmana.HRManagement.Application.Service;
using Hrmanage.HRManagement.Application.Service;
using hrmana.HRManagement.Application.DTOs;
using Hrmanagement.HRManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace hrmana.HrManagement.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _dpm;
        public DepartmentController(IDepartmentService dpm)
        {
            _dpm = dpm;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
           
                var d = await _dpm.GetAllDepartmentAsync();
                var d1 = d.Select(x => new DepartmentResponseDTO
                {
                    id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PositionCount = x.Positions.Count,
                    EmployeeCount = x.EmployeeInfos.Count
                }).ToList();
            
            return Ok(BaseResponse<List<DepartmentResponseDTO>>.Ok(d1,"Get All List Success"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var d = await _dpm.GetDepartmentById(id);
            if (d == null)
            {
                throw new Exception($"Department with id {id} Not Found");
            }
            var dto = new DepartmentResponseDTO
            {
                id = id,
                Name = d.Name,
                Description = d.Description,
                PositionCount = d.Positions.Count,
                EmployeeCount = d.EmployeeInfos.Count

            };
            return Ok(BaseResponse<DepartmentResponseDTO>.Ok(dto, "Get User By ID SuccessFully"));
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody]CreateDepartmentDTO cdd)
        {
            var dto = new Department
            {
                Name = cdd.Name,
                Description = cdd.Description,
            };
            await _dpm.CreateDepartmentAsync(dto);
            var responsedto = new DepartmentResponseDTO
            {
                id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                PositionCount = 0,
                EmployeeCount = 0
            };
            return Ok(BaseResponse<DepartmentResponseDTO>.Ok(responsedto, "Create New Department SuccessFully"));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(int id,[FromBody]UpdateDepartmentDTO dpm)
        {
            var existd = await _dpm.GetDepartmentById(id);
            if (existd == null)
            {
                throw new Exception($"Can't find department with id: {id}");
            }
            existd.Name = dpm.Name;
            existd.Description = dpm.Description;
            
             await _dpm.UpdateDepartmentAsync(existd);
            var responsedto = new DepartmentResponseDTO
            {
                id = existd.Id,
                Name = existd.Name,
                Description = existd.Description,
                PositionCount = 0,
                EmployeeCount = 0
                

            };
            return Ok(BaseResponse<DepartmentResponseDTO>.Ok(responsedto, "Update Successfully"));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _dpm.DeleteDepartmentAsync(id);
            return Ok(BaseResponse<object>.Ok(null, "Delete SuccessFully"));
        }
    }

}
