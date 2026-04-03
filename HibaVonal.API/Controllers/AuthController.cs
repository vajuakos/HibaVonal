using HibaVonal.API.Services.AuthService;
using HibaVonal.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginDTO)
        {
            return _authService.Login(loginDTO);
        }
    }
}