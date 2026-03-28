using HibaVonal.Shared.DTO;

namespace HibaVonal.API.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        Task<ServiceResponse<List<TicketDTO>>> GetTickets(int currentUserId, bool isCompleted);

        Task<ServiceResponse<bool>> AddTicket(TicketDTO ticket, int currentUserId);

        Task<ServiceResponse<bool>> UpdateTicket(int ticketId, TicketDTO ticketDto, int currentUserId);

        Task<ServiceResponse<bool>> DeleteTicket(int id, int currentUserId);

        Task<ServiceResponse<bool>> SubmitFeedback(int ticketId, TicketDTO ticketDto, int currentUserId);
    }
}