using CRM.API.Data;
using CRM.API.DTOs;
using CRM.API.Interfaces;
using CRM.API.Models;
using CRM.API.Helpers;
using BCrypt.Net;
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
            var userExists = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (userExists != null)
                return "User already exists";

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role ?? "SalesRep"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return "Invalid credentials";

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
                return "Invalid credentials";

            return _jwtHelper.GenerateToken(user);
        }
    }
}