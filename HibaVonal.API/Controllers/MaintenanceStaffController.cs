using HibaVonal.API.Services.MaintenanceStaffService;
using HibaVonal.API.Services.StudentTicketsService;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.API.Controllers
{
    [Route("api/maintenanceStaff")]
    [ApiController]
    [Authorize(Roles = $"{UserRoles.MaintenanceStaff},{UserRoles.DEV}")]

    public class MaintenanceStaffController : ApiControllerBase
    {
        private readonly IMaintenanceStaffService _maintenanceStaffService;

        public MaintenanceStaffController(IMaintenanceStaffService maintenanceStaffService)
        {
            _maintenanceStaffService = maintenanceStaffService;
        }

        [HttpGet("tickets")]
        public async Task<ServiceResponse<List<TicketDTO>>> GetAssignedTickets([FromQuery] bool isCompleted)
        {
            return await _maintenanceStaffService.GetAssignedTickets(CurrentUserId, isCompleted);
        }
    }

}
