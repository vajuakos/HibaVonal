using HibaVonal.API.Services.MaintenanceStaffService;
using HibaVonal.API.Services.StudentTicketsService;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
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

        [HttpPut("tickets/{ticketId}/resolve")]
        public async Task<ActionResult<ServiceResponse<bool>>> ResolveTicket(int ticketId, [FromBody] TicketDTO ticketDto)
        {
            var result = await _maintenanceStaffService.ResolveTicketAsync(ticketId, CurrentUserId, ticketDto.FeedbackComment);
            return Ok(result);
        }

        [HttpPost("equipment-request")]
        [Authorize(Roles = $"{UserRoles.MaintenanceStaff},{UserRoles.MaintenanceManager},{UserRoles.DEV}")]
        public async Task<ActionResult<ServiceResponse<bool>>> SubmitEquipmentRequest([FromBody] EquipmentRequestDTO dto)
        {
            var result = await _maintenanceStaffService.SubmitEquipmentRequest(dto, CurrentUserId);
            return Ok(result);
        }
    }

}
