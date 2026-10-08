using hrmana.HRManagement.Application.DTOs;
using Hrmanagement.HRManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Hrmanage.HRManagement.Application.Service
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDTO>> GetAllEmployeeAsync();
        Task<EmployeeResponseDTO> GetEmployeeById(int id);
        Task AddEmployeeAsync(EmployeeInfo employee);
        Task UpdateEmployeeAsync(EmployeeInfo employee);
        Task DeleteEmployeeAsync(int id);
        Task<PagedResult<EmployeeResponseDTO>> SearchEmployee(string? keyword, int? departmentId, int pageNumber, int pageSize);

    }
}
