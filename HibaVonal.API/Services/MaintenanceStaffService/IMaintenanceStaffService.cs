using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;

namespace HibaVonal.API.Services.MaintenanceStaffService
{
    public interface IMaintenanceStaffService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetAssignedTickets(int currentUserId, bool isCompleted);

        Task<ServiceResponse<bool>> ResolveTicketAsync(int ticketId, int currentUserId);
    }
}