using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.API.Services.MaintenanceStaffService
{
    public interface IMaintenanceStaffService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetAssignedTickets(int currentUserId, bool isCompleted);
    }
}
