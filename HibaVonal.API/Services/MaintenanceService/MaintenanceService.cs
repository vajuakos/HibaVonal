using HibaVonal.API.Data;
using HibaVonal.API.Extensions;
using HibaVonal.API.Models.Ticket;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.API.Services.MaintenanceService
{
    public class MaintenanceService : IMaintenanceService
    {
        private DataContext _context;

        public MaintenanceService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TicketDTO>> GetTickets(int currentUserId, bool isCompleted)
        {
            var query = _context.MaintenanceTickets
                .AsNoTracking()
                .Where(t => t.CreatedById == currentUserId);

            if (isCompleted)
                query = query.Where(t => t.Status == TicketStatus.Resolved);
            else
                query = query.Where(t => t.Status != TicketStatus.Resolved);

            return await query
                .Select(t => new TicketDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    RoomNumber = t.RoomNumber,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,

                    Rating = _context.Feedbacks
                        .Where(f => f.TicketId == t.Id)
                        .Select(f => (int?)f.Rating)
                        .FirstOrDefault(),

                    RatingComment = _context.Feedbacks
                        .Where(f => f.TicketId == t.Id)
                        .Select(f => f.FeedbackComment)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task AddTicket(TicketDTO ticketDto, int currentUserId)
        {
            var ticket = ticketDto.ToEntity(currentUserId);

            _context.MaintenanceTickets.Add(ticket);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTicket(TicketDTO ticketDto, int currentUserId)
        {
            var existingTicket = await _context.MaintenanceTickets
                .FirstOrDefaultAsync(t => t.Id == ticketDto.Id);

            if (existingTicket == null) return false;

            if (existingTicket.CreatedById != currentUserId) return false;

            existingTicket.Title = ticketDto.Title;
            existingTicket.Description = ticketDto.Description;
            existingTicket.RoomNumber = ticketDto.RoomNumber;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTicket(int id)
        {
            var ticket = await _context.MaintenanceTickets.FindAsync(id);

            if (ticket == null)
            {
                return false;
            }

            _context.MaintenanceTickets.Remove(ticket);

            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> SubmitFeedback(int ticketId, TicketDTO ticketDto, int currentUserId)
        {
            var ticketExists = await _context.MaintenanceTickets
                .AnyAsync(t => t.Id == ticketId && t.CreatedById == currentUserId);

            if (!ticketExists) return false;

            var alreadyRated = await _context.Feedbacks
                .AnyAsync(f => f.TicketId == ticketId);

            if (alreadyRated) return false;

            var newFeedback = new TicketFeedback
            {
                TicketId = ticketId,
                Rating = ticketDto.Rating ?? 0,
                FeedbackComment = ticketDto.RatingComment,
                CreatedAt = DateTime.Now,
                FeedbackerId = currentUserId
            };

            _context.Feedbacks.Add(newFeedback);

            var count = await _context.SaveChangesAsync();

            return count > 0;
        }
    }
}
