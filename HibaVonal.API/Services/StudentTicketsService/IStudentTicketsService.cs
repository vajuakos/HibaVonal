using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;

namespace HibaVonal.API.Services.StudentTicketsService
{
    public interface IStudentTicketsService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetTickets(int currentUserId, bool isCompleted);

        Task<ServiceResponse<bool>> AddTicket(TicketDTO ticket, int currentUserId);

        Task<ServiceResponse<bool>> UpdateTicket(int ticketId, TicketDTO ticketDto, int currentUserId);

        Task<ServiceResponse<bool>> DeleteTicket(int id, int currentUserId);

        Task<ServiceResponse<bool>> SubmitFeedback(int ticketId, TicketDTO ticketDto, int currentUserId);
    }
}