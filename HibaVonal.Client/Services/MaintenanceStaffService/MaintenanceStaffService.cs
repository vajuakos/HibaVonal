using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.MaintenanceStaffService
{
    public class MaintenanceStaffService : IMaintenanceStaffService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "api/maintenanceStaff";

        public MaintenanceStaffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<TicketDTO>>> GetAssignedTicketsAsync(bool isCompleted)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<TicketDTO>>>(
                    $"{BaseUrl}/tickets?isCompleted={isCompleted}");

                return result ?? new ServiceResponse<List<TicketDTO>>
                {
                    IsSuccess = false,
                    Message = "Nem érkezett válasz a szervertől."
                };
            }

            catch
            {
                return new ServiceResponse<List<TicketDTO>>
                {
                    IsSuccess = false,
                    Message = "Hiba történt a hibajegyek lekérésekor."
                };
            }
        }

        public async Task<ServiceResponse<bool>> ResolveTicketAsync(int ticketId)
        {
            var response = await _httpClient.PutAsync($"{BaseUrl}/tickets/{ticketId}/resolve", null);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "Hiba történt a lezárás során."
            };
        }
    }
}
