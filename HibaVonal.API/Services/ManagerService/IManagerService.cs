using HibaVonal.Shared.DTO;
using HibaVonal.Shared.Enum;

namespace HibaVonal.API.Services.ManagerService
{
    public interface IManagerService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetAllTicketsAsync(bool isCompletedTickets);

        Task<ServiceResponse<bool>> UpdateTicketStatusAsync(int ticketId, TicketStatus status);
    }
}