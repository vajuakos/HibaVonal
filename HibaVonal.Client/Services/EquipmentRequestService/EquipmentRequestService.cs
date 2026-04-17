using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.EquipmentRequestService
{
    public class EquipmentRequestService : IEquipmentRequestService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "api/equipmentRequest";

        public EquipmentRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<bool>> SubmitEquipmentRequestAsync(EquipmentRequestDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, dto);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "Hiba történt az eszközigény leadásakor."
            };
        }
    }
}