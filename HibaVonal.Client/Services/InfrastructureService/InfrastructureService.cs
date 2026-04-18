using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.InfrastructureService
{
    public class InfrastructureService : IInfrastructureService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "api/infrastructure";

        public InfrastructureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<EquipmentDTO>>> GetEquipmentsAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<EquipmentDTO>>>(
                    $"{BaseUrl}/equipments");

                return result ?? new ServiceResponse<List<EquipmentDTO>>
                {
                    IsSuccess = false,
                    Message = "Nem érkezett válasz a szervertől."
                };
            }
            catch
            {
                return new ServiceResponse<List<EquipmentDTO>>
                {
                    IsSuccess = false,
                    Message = "Hiba történt az eszközök lekérésekor."
                };
            }
        }

        public async Task<ServiceResponse<bool>> SubmitEquipmentRequestAsync(EquipmentRequestDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/equipment-request", dto);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "Hiba történt az eszközigény leadásakor."
            };
        }
    }
}
