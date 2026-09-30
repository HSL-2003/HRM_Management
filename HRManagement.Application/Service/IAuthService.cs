using hrmana.HRManagement.Application.DTOs;
using hrmana.HRManagement.Domain.Entities;

namespace hrmana.HRManagement.Application.Service
{
    public interface IAuthService
    {
        public Task<bool> HasAnyAdminAsync();
        public Task CreateFirstAdminAsync(RegistDTO rdto);
        public Task<string> LoginAdminAsync(LoginDTO ldto);
    }
}
