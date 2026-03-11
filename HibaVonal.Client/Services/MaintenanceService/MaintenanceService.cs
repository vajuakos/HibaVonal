using HibaVonal.Shared.DTO;
using MudBlazor;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.MaintenanceService
{
    public class MaintenanceService : IMaintenanceService
    {
        private const string BaseUrl = "api/maintenance";

        private readonly HttpClient _httpClient;

        public MaintenanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets)
        {
            try
            {
                var tickets = await _httpClient.GetFromJsonAsync<ServiceResponse<List<TicketDTO>>>($"{BaseUrl}/tickets?isCompleted={isCompletedTickets}");

                return tickets ?? new();
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public async Task<ServiceResponse<bool>> CreateNewTicketAsync(TicketDTO ticket)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/tickets", ticket);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }

        public async Task<ServiceResponse<bool>> UpdateTicket(TicketDTO ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/tickets/{ticket.Id}", ticket);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteTicket(int ticketId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/tickets/{ticketId}");

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }

        public async Task<ServiceResponse<bool>> RateTicket(TicketDTO ticket)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/tickets/{ticket.Id}/feedback", ticket);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            if (result == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A szerver válasza érvénytelen."
                };
            }

            return result;
        }
    }
}
