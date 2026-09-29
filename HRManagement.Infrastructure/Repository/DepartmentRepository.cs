using Hrmanagement.HRManagement.Domain.Entities;
using Hrmanagement.HRManagement.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace hrmana.HRManagement.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddDepartmentAsync(Department department)
        {
            await _context.AddAsync(department);
        }

        public void DeleteDepartment(Department department)
        {
            _context.Departments.Remove(department);
        }


        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.Include(d => d.Positions).Include(d => d.EmployeeInfos).ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.Include(d => d.Positions).Include(d => d.EmployeeInfos).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
        }

        public async Task<bool> ExistByName(string name)
        {
            {
                return await _context.Departments.AnyAsync(d => d.Name == name);
            }
        }

    }
}
