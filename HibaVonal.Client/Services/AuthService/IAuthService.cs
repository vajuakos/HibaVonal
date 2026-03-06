using HibaVonal.Shared.DTO.Authentication;

namespace HibaVonal.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginDTO);
    }
}
