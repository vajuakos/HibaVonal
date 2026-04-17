using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.DTO.User;
using HibaVonal.Shared.Enum;

namespace HibaVonal.Client.Services.ManagementService
{
    public interface IManagementService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets);

        Task<ServiceResponse<bool>> UpdateTicketStatusAsync(int ticketId, TicketStatus status);

        Task<ServiceResponse<List<UserListItemDTO>>> GetMaintenanceStaffUsersAsync();

        Task<ServiceResponse<bool>> AssignTicketAsync(int ticketId, int maintenanceStaffUserId);
    }
}