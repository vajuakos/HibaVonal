using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using HibaVonal.Shared.DTO.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Client.Services.MaintenanceStaffService
{
    public interface IMaintenanceStaffService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetAssignedTicketsAsync(bool isCompleted);

        Task<ServiceResponse<bool>> ResolveTicketAsync(int ticketId);

        Task<ServiceResponse<bool>> SubmitEquipmentRequestAsync(EquipmentRequestDTO dto);
    }
}
