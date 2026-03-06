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

        [HttpGet("tickets/all")]
        public Task<List<TicketDTO>> GetAllTickets()
        {
            return _maintenanceService.GetAllTickets(CurrentUserId);
        }

        [HttpPut("tickets/create")]
        public void CreateTicket(TicketDTO ticket)
        {
            _maintenanceService.AddTicket(ticket, CurrentUserId);
        }
    }
}
