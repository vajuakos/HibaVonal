using HibaVonal.API.Services.MaintenanceService;
using HibaVonal.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaintenanceController : ApiControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet("tickets")]
        public Task<ServiceResponse<List<TicketDTO>>> GetTickets([FromQuery] bool isCompleted)
        {
            return _maintenanceService.GetTickets(CurrentUserId, isCompleted);
        }

        [HttpPost("tickets")]
        public async Task<ServiceResponse<bool>> CreateTicket(TicketDTO ticket)
        {
            return await _maintenanceService.AddTicket(ticket, CurrentUserId);
        }

        [HttpPut("tickets/{ticketId}")]
        public async Task<ServiceResponse<bool>> UpdateTicket(int ticketId, TicketDTO ticket)
        {
            return await _maintenanceService.UpdateTicket(ticketId, ticket, CurrentUserId);
        }

        [HttpDelete("tickets/{ticketId}")]
        public async Task<ServiceResponse<bool>> DeleteTicket(int ticketId)
        {
            return await _maintenanceService.DeleteTicket(ticketId, CurrentUserId);
        }

        [HttpPost("tickets/{ticketId}/feedback")]
        public async Task<ServiceResponse<bool>> SubmitFeedback(int ticketId, TicketDTO ticket)
        {
            return await _maintenanceService.SubmitFeedback(ticketId, ticket, CurrentUserId);
        }
        [Authorize(Roles = "MaintenanceManager,Admin,DEV")]
        [HttpGet("manager/tickets")]
        public async Task<ActionResult<ServiceResponse<List<TicketDTO>>>> GetAllTicketsForManager([FromQuery] bool isCompleted = false)
        {
            var result = await _maintenanceService.GetAllTicketsForManagerAsync(isCompleted);
            return Ok(result);
        }
        [Authorize(Roles = "MaintenanceManager,Admin,DEV")]
        [HttpPut("manager/tickets/{ticketId}/status")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateTicketStatusForManager(int ticketId, [FromBody] TicketDTO ticket)
        {
            var result = await _maintenanceService.UpdateTicketStatusForManagerAsync(ticketId, ticket.Status);
            return Ok(result);
        }
    }
}
