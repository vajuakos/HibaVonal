using HibaVonal.Shared.DTO;

namespace HibaVonal.Client.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets);

        Task<ServiceResponse<bool>> CreateNewTicketAsync(TicketDTO ticket);

        Task<ServiceResponse<bool>> UpdateTicket(TicketDTO ticket);

        Task<ServiceResponse<bool>> DeleteTicket(int ticketId);

        Task<ServiceResponse<bool>> RateTicket(TicketDTO ticket);
    }
}