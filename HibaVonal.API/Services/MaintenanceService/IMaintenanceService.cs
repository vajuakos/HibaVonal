using HibaVonal.Shared.DTO;

namespace HibaVonal.API.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        public Task<ServiceResponse<List<TicketDTO>>> GetTickets(int currentUserId, bool isCompleted);

        public Task<ServiceResponse<bool>> AddTicket(TicketDTO ticket, int currentUserId);

        public Task<ServiceResponse<bool>> UpdateTicket(int ticketId, TicketDTO ticketDto, int currentUserId);

        public Task<ServiceResponse<bool>> DeleteTicket(int id, int currentUserId);

        public Task<ServiceResponse<bool>> SubmitFeedback(int ticketId, TicketDTO ticketDto, int currentUserId);

    }
}
