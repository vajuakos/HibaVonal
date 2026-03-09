using HibaVonal.Shared.DTO;

namespace HibaVonal.API.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        public Task<List<TicketDTO>> GetTickets(int currentUserId, bool isCompleted);

        public Task AddTicket(TicketDTO ticket, int currentUserId);

        public Task<bool> UpdateTicket(TicketDTO ticket, int currentUserId);

        public Task<bool> DeleteTicket(int id);

        public Task<bool> SubmitFeedback(int ticketId, TicketDTO ticketDto, int currentUserId);

    }
}
