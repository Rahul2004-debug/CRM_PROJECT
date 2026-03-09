using CRM.API.Data;

using CRM.API.DTOs;

using CRM.API.Helpers;

using CRM.API.Interfaces;

using CRM.API.Models;

using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.Google;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CRM.API.Controllers

{
    [ApiController]

    [Route("api/auth")]

    public class AuthController : ControllerBase

    {

        private readonly IAuthService _authService;

        private readonly AppDbContext _context;

        private readonly JwtHelper _jwtHelper;

        public AuthController(IAuthService authService, AppDbContext context, JwtHelper jwtHelper)

        {

            _authService = authService;

            _context = context;

            _jwtHelper = jwtHelper;

        }

        // REGISTER

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto dto)

        {

            var result = await _authService.Register(dto);

            if (result != "User registered successfully")

                return BadRequest(result);

            return Ok(result);

        }

        // LOGIN

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDto dto)

        {

            var result = await _authService.Login(dto);

            if (result.Contains("Invalid") || result.Contains("disabled"))

                return Unauthorized(result);

            return Ok(result);

        }

        // FORGOT PASSWORD

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return BadRequest("Email not registered");

            // Generate secure token
            var token = Guid.NewGuid().ToString();

            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(15);

            await _context.SaveChangesAsync();

            // Reset link
            var resetLink = $"https://localhost:7246/reset-password.html?token={token}";

            // Send email
            EmailHelper.SendOtp(email, resetLink);

            return Ok("Reset link sent to your email");
        }


        [HttpPost("verify-otp")]

        public async Task<IActionResult> VerifyOtp(string email, string otp)

        {

            var user = await _context.Users.FirstOrDefaultAsync(x =>

                x.Email == email &&

                x.ResetToken == otp &&

                x.ResetTokenExpiry > DateTime.UtcNow);

            if (user == null)

                return BadRequest("Invalid or expired OTP");

            return Ok("OTP verified");

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string newPassword)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.ResetToken == token);

            if (user == null)
                return BadRequest("Invalid token");

            if (user.ResetTokenExpiry < DateTime.UtcNow)
                return BadRequest("Token expired");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _context.SaveChangesAsync();

            return Ok("Password reset successful");
        }


        // GOOGLE LOGIN
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Auth", null, Request.Scheme);
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return BadRequest("Google authentication failed");

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                user = new User
                {
                    Name = name,
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("GoogleLoginUser"),
                    Role = "SalesRep"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            var token = _jwtHelper.GenerateToken(user);

            
        
            // redirect to dashboard page

            return Redirect($"/dashboard.html?token={token}");

        }

    }

}
