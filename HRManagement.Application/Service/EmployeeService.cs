using hrmana.HRManagement.Application.DTOs;
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
            if (employee.Phone.Length > 10)
            {
                throw new ArgumentException("Nigga stop ,de fuck u doing u already know phonenumber must be  10 digit above ,stop it cuz");
            }
            if (string.IsNullOrWhiteSpace(employee.Phone))
            {
                throw new ArgumentException("I ain't see shit nigga ");
            }
            var emp = await _iemp.ExistByPhone(employee.Phone);
            if (emp)
            {
                throw new InvalidOperationException($"Already Used This PhoneNumber : {employee.Phone} Cuzz");
            }
            
            
            
            if(employee.Age < 0 || employee.Age > 150)
            {
                throw new Exception("Invalid Age");
            }
            if(employee.Phone.Length != 10 || employee.Phone.Any(c => !char.IsDigit(c)))
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

        public async Task<List<EmployeeResponseDTO>> GetAllEmployeeAsync()
        {
            var e = await _iemp.GetAllEmployeeAsync();
            var list = e.Select(x => new EmployeeResponseDTO
            {
                Name = x.Name,
                Age = x.Age,
                Phone = x.Phone,
                Address = x.Address,
                Description = x.Description,
                HiredDate = x.HiredDate,
                DepartmentName = x.Department.Name,
                PositionName = x.Position.Name,
            }).ToList();
            return list;
        }

        public async Task<EmployeeResponseDTO> GetEmployeeById(int id)
        {
            var i = await _iemp.GetEmployeeById(id);
            if (i == null)
            {
                throw new KeyNotFoundException($"Can't Find Shit With ID : {id}");
            }
            var d = new EmployeeResponseDTO
            {
                Id = i.Id,
                Name = i.Name,
                Age = i.Age,
                Address = i.Address,
                Phone = i.Phone,
                Description = i.Description,
                HiredDate= i.HiredDate,
                DepartmentName = i.Department.Name,
                PositionName = i.Position.Name,
            };
            return d;
        }

        public async Task<PagedResult<EmployeeResponseDTO>> SearchEmployee(string? keyword, int? departmentId, int pageNumber, int pageSize)
        {
            if(pageNumber < 1) pageNumber = 1;
            if(pageSize < 1) pageSize = 1;
            if(pageSize > 50) pageSize = 50;
            var (items,total) = await _iemp.SearchEmployeeAsync(keyword, departmentId, pageNumber, pageSize);
            return new PagedResult<EmployeeResponseDTO>
            {
                Items = items.Select(x => new EmployeeResponseDTO
                {
                    Name = x.Name,
                    Age = x.Age,
                    Phone = x.Phone,
                    Address = x.Address,
                    Description = x.Description,
                    DepartmentName = x.Department.Name,
                    PositionName = x.Position.Name,
                    HiredDate = x.HiredDate,
                }).ToList(),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(total/(double)pageSize)
            };
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
