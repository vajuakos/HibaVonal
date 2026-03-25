using HibaVonal.Shared.DTO;
using HibaVonal.Shared.Enum;

namespace HibaVonal.Client.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets);

        Task<ServiceResponse<bool>> CreateNewTicketAsync(TicketDTO ticket);

        Task<ServiceResponse<bool>> UpdateTicket(TicketDTO ticket);

        Task<ServiceResponse<bool>> DeleteTicket(int ticketId);

        Task<ServiceResponse<bool>> RateTicket(TicketDTO ticket);

        Task<ServiceResponse<List<TicketDTO>>> GetManagerTicketsAsync(bool isCompletedTickets);

        Task<ServiceResponse<bool>> UpdateTicketStatusForManagerAsync(int ticketId, TicketStatus status);
    }
}
