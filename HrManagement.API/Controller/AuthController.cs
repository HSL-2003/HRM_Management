using hrmana.HRManagement.Application.Service;
using hrmana.HRManagement.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using hrmana.HRManagement.Domain.Entities;

namespace hrmana.HrManagement.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }
        [HttpPost("Create-Admin")]

        public async Task<IActionResult> CreateFirstAdmin([FromBody] RegistDTO rdto)
        {

            await _auth.CreateFirstAdminAsync(rdto);
            return Ok(BaseResponse<object>.Ok(null, "Create Admin Successfully"));
        }
        [HttpPost("Login-Admin")]

        public async Task<IActionResult> LoginAdmin([FromBody] LoginDTO ldto)
        {
            var savetoken = await _auth.LoginAsync(ldto);
            return Ok(BaseResponse<string>.Ok(savetoken, "Login SuccessFully"));
        }
        [HttpPost("Login-Normal")]
        
        public async Task<IActionResult> LoginNormal([FromBody] LoginDTO lndto)
        {
            var a = await _auth.LoginNormalAsync(lndto);
            return Ok(BaseResponse<string>.Ok(a, "Login SuccessFully"));
        }

        [HttpPost("Create-User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUserOnRole([FromBody] RegistDTO rdto, string role)
        {
            if (role != "Manager" && role != "Viewer")
            {
                throw new ArgumentException("Nigga,stop tripping and type correct mtfking role ur che-rub looking motherfucker");
            }
            await _auth.CreateUserOnRoleAsync(rdto, role);
            return Ok(BaseResponse<object>.Ok(null, "Create User Successfully"));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUSer()
        {
            var a = await _auth.GetAllUserAsync();
            return Ok(BaseResponse<List<UserResponseDTO>>.Ok(a, "Get All SuccessFully"));
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var b = await _auth.GetUserByIdAsync(id);
            return Ok(BaseResponse<UserResponseDTO>.Ok(b, $"Get User With ID : {id} SuccessFully"));
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var c = await _auth.GetUserByIdAsync(id);
            if (c == null)
            {
                throw new KeyNotFoundException("Can't Find Shit Cuz");
            }
             await _auth.DeleteUserAsync(id);
            return Ok(BaseResponse<object>.Ok(null, "Delete Successfully"));
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string id,[FromBody]UpdateUserDTO udto)
        {
            var e = await _auth.GetUserByIdAsync(id);
            if (e == null)
            {
                throw new KeyNotFoundException("Can't Find Shit Cuz");
            }
            await _auth.UpdateUserAsync(id, udto);
            
            return Ok(BaseResponse<UserResponseDTO>.Ok(e, "Update SuccessFully"));
        }
    }

}
