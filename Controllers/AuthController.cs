using CRM.API.DTOs;
using CRM.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.Register(dto);

            if (result != "User registered successfully")
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.Login(dto);

            if (result.Contains("Invalid") || result.Contains("disabled"))
                return Unauthorized(result);

            return Ok(result);
        }
    }
}