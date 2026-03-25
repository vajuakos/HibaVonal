using HibaVonal.Shared.DTO;
using HibaVonal.Shared.Enum;

namespace HibaVonal.Client.Services.ManagerService
{
    public interface IManagerService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets);

        Task<ServiceResponse<bool>> UpdateTicketStatusAsync(int ticketId, TicketStatus status);
    }
}