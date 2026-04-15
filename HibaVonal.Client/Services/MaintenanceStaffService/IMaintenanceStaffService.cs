using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using HibaVonal.Shared.DTO.Ticket;

namespace HibaVonal.Client.Services.MaintenanceStaffService
{
    public interface IMaintenanceStaffService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetAssignedTicketsAsync(bool isCompleted);

        Task<ServiceResponse<bool>> ResolveTicketAsync(int ticketId);

        Task<ServiceResponse<bool>> SubmitEquipmentRequestAsync(EquipmentRequestDTO dto);
    }
}
