using HibaVonal.API.Data;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.API.Services.MaintenanceStaffService
{
    public class MaintenanceStaffService : IMaintenanceStaffService 
    {
        private readonly DataContext _context;

        public MaintenanceStaffService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<TicketDTO>>> GetAssignedTickets(int currentUserId, bool isCompleted)
        {
            var query = _context.MaintenanceTickets
                .AsNoTracking()
                .Where(t => t.AssignedToId == currentUserId);

            if (isCompleted)
                query = query.Where(t => t.Status == TicketStatus.Resolved);
            else
                query = query.Where(t => t.Status != TicketStatus.Resolved);

            var tickets = await query
                .Select(t => new TicketDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    RoomNumber = t.RoomNumber,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    AssignedToId = t.AssignedToId,
                    CreatedByEmail = t.CreatedBy.Email,

                    Rating = _context.Feedbacks
                        .Where(f => f.TicketId == t.Id)
                        .Select(f => (int?)f.Rating)
                        .FirstOrDefault(),

                    FeedbackComment = _context.Feedbacks
                        .Where(f => f.TicketId == t.Id)
                        .Select(f => f.FeedbackComment)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return new ServiceResponse<List<TicketDTO>>
            {
                Data = tickets,
                IsSuccess = true
            };
        }

        public async Task<ServiceResponse<bool>> ResolveTicketAsync(int ticketId, int currentUserId)
        {
            var ticket = await _context.MaintenanceTickets
                .FirstOrDefaultAsync(t => t.Id == ticketId && t.AssignedToId == currentUserId);

            if (ticket == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Nem található vagy nem hozzárendelt hibajegy."
                };
            }

            ticket.Status = TicketStatus.Resolved;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                IsSuccess = true,
                Message = "Hibajegy sikeresen lezárva!"
            };
        }
    }
}
