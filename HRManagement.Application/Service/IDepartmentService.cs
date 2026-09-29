using Hrmanagement.HRManagement.Domain.Entities;

namespace hrmana.HRManagement.Application.Service
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetAllDepartmentAsync();
        public Task<Department> GetDepartmentById(int id);
        public Task<Department> CreateDepartmentAsync(Department dpm);
        Task DeleteDepartmentAsync(int id);
        Task UpdateDepartmentAsync(Department dpm);

    }
}
