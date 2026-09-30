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
        
        public async Task<IActionResult> CreateFirstAdmin([FromBody]RegistDTO rdto)
        {
          
            await _auth.CreateFirstAdminAsync(rdto);
            return Ok(BaseResponse<object>.Ok(null, "Create Admin Successfully"));
        }
        [HttpPost("Login-Admin")]
        
        public async Task<IActionResult> LoginAdmin([FromBody] LoginDTO ldto)
        {
            var savetoken = await _auth.LoginAdminAsync(ldto);
            return Ok(BaseResponse<string>.Ok(savetoken, "Login SuccessFully"));
        }
    }
}
