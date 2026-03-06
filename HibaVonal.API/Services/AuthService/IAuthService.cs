using HibaVonal.Shared.DTO.Authentication;

namespace HibaVonal.API.Services.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginDTO);
    }
}
