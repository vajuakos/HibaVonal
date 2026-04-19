using Blazored.LocalStorage;
using HibaVonal.Shared.DTO.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private const string BaseUrl = "api/auth";

        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(ILocalStorageService localStorage, HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", loginDTO);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                if (result != null && result.IsSuccess)
                {
                    await _localStorage.SetItemAsync("authToken", result.Token);

                    await ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserAuthenticatedAsync(result.Token);

                    return result;
                }

                return new LoginResponseDTO { IsSuccess = false, Token = null };
            }

            return new LoginResponseDTO { IsSuccess = false, Token = null };
        }
    }
}
