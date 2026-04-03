using HibaVonal.API.Data;
using HibaVonal.API.Extensions;
using HibaVonal.API.Models.Ticket;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.API.Services.StudentTicketsService
{
    public class StudentTicketsService : IStudentTicketsService
    {
        private DataContext _context;

        public StudentTicketsService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<TicketDTO>>> GetTickets(int currentUserId, bool isCompleted)
        {
            var query = _context.MaintenanceTickets
                .AsNoTracking()
                .Where(t => t.CreatedById == currentUserId);

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

        public async Task<ServiceResponse<bool>> AddTicket(TicketDTO ticketDto, int currentUserId)
        {
            var ticket = ticketDto.ToEntity(currentUserId);

            if (ticket == null) return new ServiceResponse<bool> { IsSuccess = false, Message = "Sikertelen rögzítés!" };

            _context.MaintenanceTickets.Add(ticket);

            var count = await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { IsSuccess = count > 0, Message = "Sikeres rögzítés!" };
        }

        public async Task<ServiceResponse<bool>> UpdateTicket(int ticketId, TicketDTO ticketDto, int currentUserId)
        {
            var existingTicket = await _context.MaintenanceTickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (existingTicket == null) return new ServiceResponse<bool> { IsSuccess = false, Message = "Nem található hibajegy!" };

            if (existingTicket.CreatedById != currentUserId) return new ServiceResponse<bool> { IsSuccess = false, Message = "Nem jogosult a módosításra!" };

            existingTicket.Title = ticketDto.Title;
            existingTicket.Description = ticketDto.Description;
            existingTicket.RoomNumber = ticketDto.RoomNumber;

            var count = await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { IsSuccess = count > 0, Message = "Sikeres módosítás!" };
        }

        public async Task<ServiceResponse<bool>> DeleteTicket(int id, int currentUserId)
        {
            var ticket = await _context.MaintenanceTickets.FindAsync(id);

            if (ticket == null) return new ServiceResponse<bool> { IsSuccess = false, Message = "Nem található hibajegy!" };

            if (ticket.CreatedById != currentUserId) return new ServiceResponse<bool> { IsSuccess = false, Message = "Nem jogosult a törlésre!" };

            _context.MaintenanceTickets.Remove(ticket);

            var affectedRows = await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { IsSuccess = affectedRows > 0, Message = "Sikeres törlés!" };
        }

        public async Task<ServiceResponse<bool>> SubmitFeedback(int ticketId, TicketDTO ticketDto, int currentUserId)
        {
            var ticketExists = await _context.MaintenanceTickets
                .AnyAsync(t => t.Id == ticketId && t.CreatedById == currentUserId);

            if (!ticketExists) return new ServiceResponse<bool> { IsSuccess = false, Message = "Nem található a hibajegy!" };

            var alreadyRated = await _context.Feedbacks
                .AnyAsync(f => f.TicketId == ticketId);

            if (alreadyRated) return new ServiceResponse<bool> { IsSuccess = false, Message = "A hibajegy már értékelésre került!" };

            var newFeedback = new TicketFeedback
            {
                TicketId = ticketId,
                Rating = ticketDto.Rating ?? 0,
                FeedbackComment = ticketDto.FeedbackComment,
                CreatedAt = DateTime.Now,
                FeedbackerId = currentUserId
            };

            _context.Feedbacks.Add(newFeedback);

            var count = await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { IsSuccess = count > 0, Message = "Sikeres rögzítés!" };
        }
    }
}