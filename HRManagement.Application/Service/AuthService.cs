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
                throw new Exception("create successfully But not allow admin,created fail");
            }

        }

        public async Task<bool> HasAnyAdminAsync()
        {
            var ad = await _usermana.GetUsersInRoleAsync("Admin");
            return ad.Any();
        }

        public async Task<string> LoginAdminAsync(LoginDTO ldto)
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
  
        }
    }

