using HibaVonal.API.Data;
using HibaVonal.API.Extensions;
using HibaVonal.Shared.DTO;
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

        public async Task<List<TicketDTO>> GetAllTickets(int currentUserId)
        {
            return await _context.MaintenanceTickets
                .AsNoTracking()
                .Where(t => t.CreatedById == currentUserId)
                .Select(t => new TicketDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
        }

        public async Task AddTicket(TicketDTO ticketDto, int currentUserId)
        {
            var ticket = ticketDto.ToEntity(currentUserId);

            _context.MaintenanceTickets.Add(ticket);

            await _context.SaveChangesAsync();
        }

        public void DeleteTicket(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTicket(TicketDTO ticket)
        {
            throw new NotImplementedException();
        }
    }
}
