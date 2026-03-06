using HibaVonal.Shared.DTO;

namespace HibaVonal.Client.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        Task<List<TicketDTO>> GetTicketsAsync();

        Task CreateNewTicketAsync(TicketDTO ticket);

        void DeleteTicket(int id);

        void UpdateTicket(TicketDTO ticket);
    }
}
