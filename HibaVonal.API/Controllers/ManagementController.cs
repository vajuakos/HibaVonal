using HibaVonal.API.Services.ManagementService;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{UserRoles.MaintenanceManager},{UserRoles.DEV}")]
    public class ManagementController : ApiControllerBase
    {
        private readonly IManagementService _managementService;

        public ManagementController(IManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpGet("tickets")]
        public async Task<ActionResult<ServiceResponse<List<TicketDTO>>>> GetAllTickets([FromQuery] bool isCompleted = false)
        {
            var result = await _managementService.GetAllTicketsAsync(isCompleted);
            return Ok(result);
        }

        [HttpPut("tickets/{ticketId}/status")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateTicketStatus(int ticketId, [FromBody] TicketDTO ticket)
        {
            var result = await _managementService.UpdateTicketStatusAsync(ticketId, ticket.Status);
            return Ok(result);
        }

        [HttpGet("staff")]
        public async Task<ActionResult<ServiceResponse<List<UserListItemDTO>>>> GetMaintenanceStaffUsers()
        {
            var result = await _managementService.GetMaintenanceStaffUsersAsync();
            return Ok(result);
        }

        [HttpPut("tickets/{ticketId}/assign")]
        public async Task<ActionResult<ServiceResponse<bool>>> AssignTicket(int ticketId, [FromBody] AssignTicketRequestDTO request)
        {
            var result = await _managementService.AssignTicketAsync(ticketId, request.MaintenanceStaffUserId);
            return Ok(result);
        }
    }
}