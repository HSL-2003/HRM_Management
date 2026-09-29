using Hrmanagement.HRManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrmanagement.HRManagement.Infrastructure.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeInfo>> GetAllEmployeeAsync();
        Task<EmployeeInfo> GetEmployeeById(int id);
        Task AddEmployeeAsync(EmployeeInfo employee);
        void UpdateEmployee(EmployeeInfo employee);
        void DeleteEmployee(EmployeeInfo employee);
        Task<bool> ExistByPhone(string phone);

        Task Savechangeasync();
    }
}
