using HibaVonal.API.Services.StudentTicketsService;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.API.Controllers
{
    [Route("api/student")]
    [ApiController]
    [Authorize(Roles = $"{UserRoles.Student},{UserRoles.DEV}")]
    public class StudentTicketsController : ApiControllerBase
    {
        private readonly IStudentTicketsService _studentTicketsService;

        public StudentTicketsController(IStudentTicketsService studentTicketsService)
        {
            _studentTicketsService = studentTicketsService;
        }

        [HttpGet("tickets")]
        public async Task<ServiceResponse<List<TicketDTO>>> GetTickets([FromQuery] bool isCompleted)
        {
            return await _studentTicketsService.GetTickets(CurrentUserId, isCompleted);
        }

        [HttpPost("tickets")]
        public async Task<ServiceResponse<bool>> CreateTicket(TicketDTO ticket)
        {
            return await _studentTicketsService.AddTicket(ticket, CurrentUserId);
        }

        [HttpPut("tickets/{ticketId}")]
        public async Task<ServiceResponse<bool>> UpdateTicket(int ticketId, TicketDTO ticket)
        {
            return await _studentTicketsService.UpdateTicket(ticketId, ticket, CurrentUserId);
        }

        [HttpDelete("tickets/{ticketId}")]
        public async Task<ServiceResponse<bool>> DeleteTicket(int ticketId)
        {
            return await _studentTicketsService.DeleteTicket(ticketId, CurrentUserId);
        }

        [HttpPost("tickets/{ticketId}/feedback")]
        public async Task<ServiceResponse<bool>> SubmitFeedback(int ticketId, TicketDTO ticket)
        {
            return await _studentTicketsService.SubmitFeedback(ticketId, ticket, CurrentUserId);
        }
    }
}