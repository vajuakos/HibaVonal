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
        public Task<List<TicketDTO>> GetTickets([FromQuery] bool isCompleted)
        {
            return _maintenanceService.GetTickets(CurrentUserId, isCompleted);
        }

        [HttpPut("tickets/create")]
        public void CreateTicket(TicketDTO ticket)
        {
            _maintenanceService.AddTicket(ticket, CurrentUserId);
        }

        [HttpPut("tickets/update")]
        public async Task<bool> UpdateTicket(TicketDTO ticket)
        {
            return await _maintenanceService.UpdateTicket(ticket, CurrentUserId);
        }

        [HttpPost("tickets/delete")]
        public async Task<bool> DeleteTicket([FromBody] int ticketId)
        {
            return await _maintenanceService.DeleteTicket(ticketId);
        }

        [HttpPost("tickets/{ticketId}/feedback")]
        public async Task<bool> SubmitFeedback(int ticketId, TicketDTO ticket)
        {
            return await _maintenanceService.SubmitFeedback(ticketId, ticket, CurrentUserId);
        }
    }
}
