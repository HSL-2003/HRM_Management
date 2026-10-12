using hrmana.HRManagement.Infrastructure.Repository;
using Hrmanagement.HRManagement.Domain.Entities;

namespace hrmana.HRManagement.Application.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _dpms;
        public DepartmentService(IDepartmentRepository dpms)
        {
            _dpms = dpms;
        }

        public async Task<Department> CreateDepartmentAsync(Department dpm)
        {
            bool isExist = await _dpms.ExistByName(dpm.Name);
            if (isExist == true)
            {
                throw new InvalidOperationException("Duplicate name");
            }
            await _dpms.AddDepartmentAsync(dpm);
            await _dpms.SaveChangesAsync();
            return dpm;
        }


        public async Task DeleteDepartmentAsync(int id)
        {
            var d = await _dpms.GetDepartmentByIdAsync(id);
            if (d == null)
            {
                throw new Exception($" Can't see department with {id} ");

            }
             _dpms.DeleteDepartment(d);
            await _dpms.SaveChangesAsync();

        }


        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            return await _dpms.GetAllDepartmentsAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var d = await _dpms.GetDepartmentByIdAsync(id);
            if (d == null)
            {
                throw new Exception("Ain't got no department here sir");
            }
            else
            {
                return d;
            }

        }

        public async Task UpdateDepartmentAsync(Department dpm)
        {
            var d = await _dpms.GetDepartmentByIdAsync(dpm.Id);
            if (d != null)
            {
                _dpms.UpdateDepartment(dpm);
               await _dpms.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Can't find shit with id {dpm.Id}");
            }
        }
        
    }
}

