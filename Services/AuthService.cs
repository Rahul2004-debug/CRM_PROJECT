using CRM.API.Data;
using CRM.API.DTOs;
using CRM.API.Interfaces;
using CRM.API.Models;
using CRM.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(AppDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            // Name validation (letters, numbers, underscore only)
            var nameRegex = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9_]+$");

            if (!nameRegex.IsMatch(dto.Name))
                return "Name can contain only letters, numbers and underscore (_)";

            dto.Email = dto.Email.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                return "All fields are required";

            // Gmail validation
            if (!dto.Email.EndsWith("@gmail.com"))
                return "Only Gmail addresses allowed";

            // Password validation
            var passwordRegex = new System.Text.RegularExpressions.Regex(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$");

            if (!passwordRegex.IsMatch(dto.Password))
                return "Password must contain 8 characters, 1 uppercase, 1 number and 1 special character";

            if (dto.Password != dto.ConfirmPassword)
                return "Passwords do not match";

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
                return "Email already registered";

            var user = new User
            {
                Name = dto.Name.Trim(),
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = string.IsNullOrEmpty(dto.Role) ? "SalesRep" : dto.Role
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<string> Login(LoginDto dto)
        {
            dto.Email = dto.Email.Trim().ToLower();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return "Invalid email or password";

            if (!user.IsActive)
                return "Account disabled";

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 5)
                    user.IsActive = false;

                await _context.SaveChangesAsync();

                return "Invalid email or password";
            }

            user.FailedLoginAttempts = 0;

            await _context.SaveChangesAsync();

            return _jwtHelper.GenerateToken(user);
        }
    }
}