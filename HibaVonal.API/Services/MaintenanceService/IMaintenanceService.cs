using HibaVonal.Shared.DTO;

namespace HibaVonal.API.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        public Task<List<TicketDTO>> GetAllTickets(int currentUserId);

        public Task AddTicket(TicketDTO ticket, int currentUserId);

        public void UpdateTicket(TicketDTO ticket);

        public Task<bool> DeleteTicket(int id);
    }
}
