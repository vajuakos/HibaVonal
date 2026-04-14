using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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

                return result ?? new();
            }

            catch
            {
                return new();
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
