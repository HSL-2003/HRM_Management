using Hrmanagement.HRManagement.Domain.Entities;
using Hrmanagement.HRManagement.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrmanagement.HRManagement.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeInfo>> GetAllEmployeeAsync()
        {
            return await _context.EmployeeInfos.Include(e => e.Department).Include(e=>e.Position).ToListAsync();
        }
        public async Task<EmployeeInfo> GetEmployeeById(int id)
        {
            return await _context.EmployeeInfos.Include(e=>e.Department).Include(e=>e.Position).FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task AddEmployeeAsync(EmployeeInfo employee)
        {
            await _context.EmployeeInfos.AddAsync(employee);
        }
        public void UpdateEmployee(EmployeeInfo employee)
        {
            _context.EmployeeInfos.Update(employee);
        }
        public void DeleteEmployee(EmployeeInfo employee)
        {
            _context.EmployeeInfos.Remove(employee);
        }
        public async Task Savechangeasync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistByPhone(string phone)
        {
            var emp = await _context.EmployeeInfos.FirstOrDefaultAsync(e => e.Phone == phone);
            return emp != null;
        }
    }
}
