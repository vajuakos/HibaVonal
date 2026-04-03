using HibaVonal.API.Services.ManagementService;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
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
    }
}