using HibaVonal.Shared.DTO;
using MudBlazor;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.MaintenanceService
{
    public class MaintenanceService : IMaintenanceService
    {
        private const string BaseUrl = "api/maintenance";

        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public MaintenanceService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<List<TicketDTO>> GetTicketsAsync(bool isCompletedTickets)
        {
            try
            {
                var tickets = await _httpClient.GetFromJsonAsync<List<TicketDTO>>($"{BaseUrl}/tickets?isCompleted={isCompletedTickets}");

                return tickets ?? new();
            }
            catch (Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);

                return new();
            }
        }

        public async Task CreateNewTicketAsync(TicketDTO ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/tickets/create", ticket);

            if (response.IsSuccessStatusCode)
                _snackbar.Add("Hiba sikeresen létrehozva!", Severity.Success);
            else
                _snackbar.Add("Hiba történt a hiba létrehozásakor.", Severity.Error);
        }

        public async Task<bool> UpdateTicket(TicketDTO ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/tickets/update", ticket);

            bool result = false;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<bool>();
            }

            if (result)
                _snackbar.Add("Hiba sikeresen törölve!", Severity.Success);
            else
                _snackbar.Add("Hiba történt a hiba törlésekor.", Severity.Error);

            return result;
        }

        public async Task<bool> DeleteTicket(int ticketId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/tickets/delete", ticketId);

            bool result = false;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<bool>();
            }

            if (result)
                _snackbar.Add("Hiba sikeresen törölve!", Severity.Success);
            else
                _snackbar.Add("Hiba történt a hiba törlésekor.", Severity.Error);

            return result;
        }

        public async Task<bool> RateTicket(TicketDTO ticket)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/tickets/{ticket.Id}/feedback", ticket);

            bool result = false;

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<bool>();
            }

            return result;
        }
    }
}
