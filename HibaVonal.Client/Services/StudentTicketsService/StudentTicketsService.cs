using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.StudentTicketsService
{
    public class StudentTicketsService : IStudentTicketsService
    {
        private const string BaseUrl = "api/student/tickets";

        private readonly HttpClient _httpClient;

        public StudentTicketsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets)
        {
            try
            {
                var tickets = await _httpClient.GetFromJsonAsync<ServiceResponse<List<TicketDTO>>>(
                    $"{BaseUrl}?isCompleted={isCompletedTickets}");

                return tickets ?? new();
            }
            catch
            {
                return new();
            }
        }

        public async Task<ServiceResponse<bool>> CreateNewTicketAsync(TicketDTO ticket)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}", ticket);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }

        public async Task<ServiceResponse<bool>> UpdateTicket(TicketDTO ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{ticket.Id}", ticket);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteTicket(int ticketId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{ticketId}");

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();

            return result ?? new ServiceResponse<bool>
            {
                IsSuccess = false,
                Message = "A szerver válasza érvénytelen."
            };
        }

        public async Task<ServiceResponse<bool>> SubmitFeedback(TicketDTO ticket)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{ticket.Id}/feedback", ticket);

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