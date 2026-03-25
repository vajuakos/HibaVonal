using HibaVonal.API.Data;
using HibaVonal.API.Services.AuthService;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SQLitePCL;
using System.ComponentModel.DataAnnotations;

namespace HibaVonal.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly DataContext _context;
        public AuthController(IAuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginDTO)
        {
            return _authService.Login(loginDTO);
        }
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.UserName, // Az IdentityUser-ben UserName van, ezt használjuk
                    Role = "Admin"
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}