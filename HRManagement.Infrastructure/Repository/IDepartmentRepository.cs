using Hrmanagement.HRManagement.Domain.Entities;

namespace hrmana.HRManagement.Infrastructure.Repository
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetAllDepartmentsAsync();
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task AddDepartmentAsync(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);
        public Task<bool> ExistByName(string name);
        Task SaveChangesAsync();
    }
}
