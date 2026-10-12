using hrmana.HRManagement.Application.DTOs;
using hrmana.HRManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace hrmana.HRManagement.Application.Service
{
    public interface IAuthService
    {
        public Task<bool> HasAnyAdminAsync();
        public Task CreateFirstAdminAsync(RegistDTO rdto);
        public Task<string> LoginAsync(LoginDTO ldto);
        public Task<string> LoginNormalAsync(LoginDTO lndto);
        public Task CreateUserOnRoleAsync(RegistDTO rdto, string role);
        public Task<List<UserResponseDTO>> GetAllUserAsync();
        public Task<UserResponseDTO> GetUserByIdAsync(string id);
        public Task UpdateUserAsync(string id,UpdateUserDTO udto);
        public Task DeleteUserAsync(string id);
    }
}
