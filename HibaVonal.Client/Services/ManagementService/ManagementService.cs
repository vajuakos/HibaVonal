using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.Enum;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.ManagementService
{
    public class ManagementService : IManagementService
    {
        private const string BaseUrl = "api/management";

        private readonly HttpClient _httpClient;

        public ManagementService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets)
        {
            try
            {
                var tickets = await _httpClient.GetFromJsonAsync<ServiceResponse<List<TicketDTO>>>(
                    $"{BaseUrl}/tickets?isCompleted={isCompletedTickets}");

                return tickets ?? new();
            }
            catch
            {
                return new();
            }
        }

        public async Task<ServiceResponse<bool>> UpdateTicketStatusAsync(int ticketId, TicketStatus status)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{BaseUrl}/tickets/{ticketId}/status",
                new TicketDTO { Status = status });

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }
    }
}