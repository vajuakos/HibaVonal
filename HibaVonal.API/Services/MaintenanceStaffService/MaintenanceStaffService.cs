using HibaVonal.API.Data;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
