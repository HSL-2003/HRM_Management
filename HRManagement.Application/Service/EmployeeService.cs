using Hrmanagement.HRManagement.Domain.Entities;
using Hrmanagement.HRManagement.Infrastructure.Repository;

namespace Hrmanage.HRManagement.Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _iemp;
        public EmployeeService (IEmployeeRepository iemp)
        {
            _iemp = iemp;
        }
        public async Task AddEmployeeAsync(EmployeeInfo employee)
        {
            var emp = await _iemp.GetEmployeeById(employee.Id);
            if (emp != null) {
                throw new Exception($"Employee with ID {employee.Id} already exists.");
            }
            if(employee.Age < 0 || employee.Age > 150)
            {
                throw new Exception("Invalid Age");
            }
            if(employee.Phone.Length != 10 && employee.Phone.Any(c => !char.IsDigit(c)))
            {
                throw new Exception("Invalid Phone Number");
            }
            await _iemp.AddEmployeeAsync(employee);
            await _iemp.Savechangeasync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var e = await _iemp.GetEmployeeById(id);
            if(e != null)
            {
                _iemp.DeleteEmployee(e);
                await _iemp.Savechangeasync();
            }
            else
            {
                throw new Exception("Can't Find Shit Cuz It's Not Here");
            }
        }

        public async Task<List<EmployeeInfo>> GetAllEmployeeAsync()
        {
            return await _iemp.GetAllEmployeeAsync();
        }

        public async Task<EmployeeInfo> GetEmployeeById(int id)
        {
            var i = await _iemp.GetEmployeeById(id);
            if (i != null) {
                return i;
            }
            else
            {
                throw new Exception("u gotta wrong cuzz");
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var i = await _iemp.GetEmployeeById(employeeInfo.Id);
            if(i != null)
            {
                _iemp.UpdateEmployee(employeeInfo);
                await _iemp.Savechangeasync();
            }
            else
            {
                throw new Exception("yo Piece u wanna come out here");
            }

        }
    }
}
