using HibaVonal.API.Models;
using HibaVonal.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HibaVonal.API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private UserManager<AppUser> _userManager;
        private IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null) return new LoginResponseDTO { IsSuccess = false };

            var result = await _userManager.CheckPasswordAsync(user!, loginDTO.Password);

            if (!result) return new LoginResponseDTO { IsSuccess = false };

            return new LoginResponseDTO { IsSuccess = true, Token = GenerateToken(user) };
        }

        public string GenerateToken(AppUser user)
        {
            var key = _configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email!),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
