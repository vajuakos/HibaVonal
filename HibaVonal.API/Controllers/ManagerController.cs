using HibaVonal.API.Services.ManagerService;
using HibaVonal.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "MaintenanceManager,Admin,DEV")]
    public class ManagerController : ApiControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet("tickets")]
        public async Task<ActionResult<ServiceResponse<List<TicketDTO>>>> GetAllTickets([FromQuery] bool isCompleted = false)
        {
            var result = await _managerService.GetAllTicketsAsync(isCompleted);
            return Ok(result);
        }

        [HttpPut("tickets/{ticketId}/status")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateTicketStatus(int ticketId, [FromBody] TicketDTO ticket)
        {
            var result = await _managerService.UpdateTicketStatusAsync(ticketId, ticket.Status);
            return Ok(result);
        }
    }
}