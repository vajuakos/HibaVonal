using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;

namespace HibaVonal.Client.Services.StudentTicketsService
{
    public interface IStudentTicketsService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetTicketsAsync(bool isCompletedTickets);

        Task<ServiceResponse<bool>> CreateNewTicketAsync(TicketDTO ticket);

        Task<ServiceResponse<bool>> UpdateTicket(TicketDTO ticket);

        Task<ServiceResponse<bool>> DeleteTicket(int ticketId);

        Task<ServiceResponse<bool>> SubmitFeedback(TicketDTO ticket);
    }
}