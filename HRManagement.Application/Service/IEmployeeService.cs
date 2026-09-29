using Hrmanagement.HRManagement.Domain.Entities;
namespace Hrmanage.HRManagement.Application.Service
{
    public interface IEmployeeService
    {
        Task<List<EmployeeInfo>> GetAllEmployeeAsync();
        Task<EmployeeInfo> GetEmployeeById(int id);
        Task AddEmployeeAsync(EmployeeInfo employee);
        Task UpdateEmployeeAsync(EmployeeInfo employee);
        Task DeleteEmployeeAsync(int id);

    }
}
