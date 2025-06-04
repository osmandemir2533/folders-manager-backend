using Folders_Auction_Business.Services;
using Folders_Auction_Core.DTOs;
using Folders_Auction_Core.Entities;
using Folders_Auction_Data_Access.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Folders_Auction_Business.Managers
{
    public class AuthManager : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthManager(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> RegisterAsync(UserRegisterDto dto)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new AppUser { Username = dto.Username, Password = hashed };
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwt(user);
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var user = _context.AppUsers.FirstOrDefault(x => x.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new Exception("Hatalı giriş");

            return GenerateJwt(user);
        }

        private string GenerateJwt(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
