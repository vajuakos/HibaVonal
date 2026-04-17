using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.DTO.User;
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

                return tickets ?? new ServiceResponse<List<TicketDTO>>
                {
                    IsSuccess = false,
                    Message = "A szerver válasza üres volt."
                };
            }
            catch
            {
                return new ServiceResponse<List<TicketDTO>>
                {
                    IsSuccess = false,
                    Message = "Hiba történt a ticketek betöltésekor."
                };
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

        public async Task<ServiceResponse<List<UserListItemDTO>>> GetMaintenanceStaffUsersAsync()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<ServiceResponse<List<UserListItemDTO>>>(
                    $"{BaseUrl}/staff");

                return users ?? new ServiceResponse<List<UserListItemDTO>>
                {
                    IsSuccess = false,
                    Message = "A szerver válasza üres volt."
                };
            }
            catch
            {
                return new ServiceResponse<List<UserListItemDTO>>
                {
                    IsSuccess = false,
                    Message = "Hiba történt a karbantartók betöltésekor."
                };
            }
        }

        public async Task<ServiceResponse<bool>> AssignTicketAsync(int ticketId, int maintenanceStaffUserId)
        {
            var request = new AssignTicketRequestDTO
            {
                MaintenanceStaffUserId = maintenanceStaffUserId
            };

            var response = await _httpClient.PutAsJsonAsync(
                $"{BaseUrl}/tickets/{ticketId}/assign",
                request);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }
    }
}