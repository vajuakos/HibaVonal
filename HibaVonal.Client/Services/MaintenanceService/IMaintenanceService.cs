using HibaVonal.Shared.DTO;

namespace HibaVonal.Client.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        Task<List<TicketDTO>> GetTicketsAsync();

        Task CreateNewTicketAsync(TicketDTO ticket);

        Task<bool> DeleteTicket(int ticketId);

        void UpdateTicket(TicketDTO ticket);
    }
}
