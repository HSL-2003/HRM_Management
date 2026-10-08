using hrmana.HRManagement.Application.DTOs;
using hrmana.HRManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hrmana.HRManagement.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _usermana;
        private readonly RoleManager<IdentityRole> _rolemana;
        private readonly IConfiguration _config;
        public AuthService(UserManager<AppUser> usermana,RoleManager<IdentityRole> rolemana, IConfiguration config)
        {
            _usermana = usermana;
            _rolemana = rolemana;
            _config = config;
        }

        public async Task CreateFirstAdminAsync(RegistDTO rdto)
        {
            if (await HasAnyAdminAsync())
            {
                throw new Exception("Already Have Account");
            }
            var e = new AppUser
            {
                UserName = rdto.Email,
                FullName = rdto.FullName,
                Email = rdto.Email,
                PhoneNumber = rdto.PhoneNumber,
            };
            var cfa = await _usermana.CreateAsync(e,rdto.Password);
            if (!cfa.Succeeded)
            {
                var error = cfa.Errors.Select(e => e.Description).ToList();
                throw new Exception(string.Join(",", error));
            }
            var roleresult = await _usermana.AddToRoleAsync(e, "Admin");
            if (!roleresult.Succeeded)
            {
                await _usermana.DeleteAsync(e);
                throw new InvalidOperationException("create successfully But not allow admin,created fail");
            }

        }

        public async Task<bool> HasAnyAdminAsync()
        {
            var ad = await _usermana.GetUsersInRoleAsync("Admin");
            return ad.Any();
        }

        public async Task<string> LoginAsync(LoginDTO ldto)
        {
            var a = await _usermana.FindByEmailAsync(ldto.Email);
            if (a == null)
            {
                throw new KeyNotFoundException("Can't find shit cuz");
            }
            else
            {
                var checkpw = await _usermana.CheckPasswordAsync(a, ldto.Password);
                if (checkpw == false)
                {
                    throw new ArgumentException("Wrong Password u piece of shit");
                }
                var c = await _usermana.IsInRoleAsync(a, "Admin");
                if (c != true)
                {
                    throw new UnauthorizedAccessException("Not An Admin,De Fuck u doing here cuz");
                }
            }
            var role = await _usermana.GetRolesAsync(a);
            return GenerateJwtToken(a,role);


        }

        public async Task CreateUserOnRoleAsync(RegistDTO rdto, string role)
        {
            var e = new AppUser
            {
                FullName = rdto.FullName,
                Email = rdto.Email,
                UserName = rdto.Email,
                PhoneNumber = rdto.PhoneNumber,
            };
            var cu = await _usermana.CreateAsync(e, rdto.Password);
            if (!cu.Succeeded)
            {
                var error = cu.Errors.Select(x => x.Description).ToList();
                throw new ArgumentException(string.Join(",",error));
            }
            var roleresult = await _usermana.AddToRoleAsync(e, role);
            if (!roleresult.Succeeded)
            {
                var error = await _usermana.DeleteAsync(e);
                throw new InvalidOperationException("Create SuccessFully But Atomicity");
            }
        }
        
        private string GenerateJwtToken(AppUser user, IList<string> roles)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email)
            };
            foreach (var rolename in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, rolename));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
           audience: _config["Jwt:Audience"],
           claims: claim,
           expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpireMinutes"])),
           signingCredentials: cred
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<UserResponseDTO>> GetAllUserAsync()
        {
            var e = _usermana.Users.ToList();
            var d = new List<UserResponseDTO>();
            foreach (var u in e)
            {
                var role = await _usermana.GetRolesAsync(u);
                d.Add(new UserResponseDTO {

                    Id = u.Id,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = role.FirstOrDefault()

                });
            }
            return d;
            
        }
        

        public async Task<UserResponseDTO> GetUserByIdAsync(string id)
        {
            var e = await _usermana.FindByIdAsync(id);
            if (e == null)
            {
                throw new KeyNotFoundException($"Can't Find Shit With id :{id}");
            }
            var d = await _usermana.GetRolesAsync(e);
            return new UserResponseDTO
            {
                Id = e.Id,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                FullName = e.FullName,
                Role = d.FirstOrDefault()
            };
            
        }

        public async Task UpdateUserAsync(string id,UpdateUserDTO udto)
        {
            var e = await _usermana.FindByIdAsync(id);
            if (e == null)
            {
                throw new KeyNotFoundException($"Can't Find Shit Cuz,This ID Not Event Exist : {id}");
            }
            else
            {
                e.FullName = udto.FullName;
                e.PhoneNumber = udto.PhoneNumber;
            }
                
            var udu = await _usermana.UpdateAsync(e);
            if (!udu.Succeeded)
            {
                var error = udu.Errors.Select(x => x.Description).ToList();
                throw new ArgumentException(string.Join(",", error));
            }
            
            
        }

        public async Task DeleteUserAsync(string id)
        {
            var e = await _usermana.FindByIdAsync(id);
            if (e == null)
            {
                throw new KeyNotFoundException($"Can't Find Shit Cuz,This ID Not Event Exist : {id}");
            }
            var crole = await _usermana.IsInRoleAsync(e, "Admin");
            if (crole)
            {
                throw new InvalidOperationException("Can't Get In Here Cuz");
            }
            else
            {
                var result = await _usermana.DeleteAsync(e);
                if (!result.Succeeded)
                {

                }
            }
            
            

        }
    }
    }

