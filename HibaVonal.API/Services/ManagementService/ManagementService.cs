using HibaVonal.API.Data;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Ticket;
using HibaVonal.Shared.DTO.User;
using HibaVonal.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.API.Services.ManagementService
{
    public class ManagementService : IManagementService
    {
        private readonly DataContext _context;

        public ManagementService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<TicketDTO>>> GetAllTicketsAsync(bool isCompletedTickets)
        {
            var query = _context.MaintenanceTickets
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Review)
                .AsQueryable();

            if (isCompletedTickets)
            {
                query = query.Where(t => t.Status == TicketStatus.Resolved);
            }
            else
            {
                query = query.Where(t => t.Status != TicketStatus.Resolved);
            }

            var tickets = await query
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TicketDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    RoomNumber = t.RoomNumber,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    AssignedToId = t.AssignedToId,
                    AssignedToEmail = t.AssignedTo != null ? t.AssignedTo.Email : null,
                    CreatedByEmail = t.CreatedBy.Email,
                    Rating = t.Review != null ? t.Review.Rating : null,
                    FeedbackComment = t.Review != null ? t.Review.FeedbackComment : null
                })
                .ToListAsync();

            return new ServiceResponse<List<TicketDTO>>
            {
                Data = tickets,
                IsSuccess = true
            };
        }

        public async Task<ServiceResponse<bool>> UpdateTicketStatusAsync(int ticketId, TicketStatus status)
        {
            var ticket = await _context.MaintenanceTickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A ticket nem található.",
                    Data = false
                };
            }

            ticket.Status = status;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                IsSuccess = true,
                Message = "A ticket státusza frissítve lett.",
                Data = true
            };
        }

        public async Task<ServiceResponse<List<UserListItemDTO>>> GetMaintenanceStaffUsersAsync()
        {
            var staffUsers = await (
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                join role in _context.Roles on userRole.RoleId equals role.Id
                where role.Name == UserRoles.MaintenanceStaff
                orderby user.Email
                select new UserListItemDTO
                {
                    Id = user.Id,
                    Email = user.Email!
                }
            ).ToListAsync();

            return new ServiceResponse<List<UserListItemDTO>>
            {
                IsSuccess = true,
                Data = staffUsers
            };
        }

        public async Task<ServiceResponse<bool>> AssignTicketAsync(int ticketId, int maintenanceStaffUserId)
        {
            var ticket = await _context.MaintenanceTickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A kiválasztott hibajegy nem található.",
                    Data = false
                };
            }

            if (ticket.Status == TicketStatus.Resolved)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Lezárt hibajegyet nem lehet karbantartóhoz rendelni.",
                    Data = false
                };
            }

            var userExists = await _context.Users.AnyAsync(u => u.Id == maintenanceStaffUserId);

            if (!userExists)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A kiválasztott felhasználó nem található.",
                    Data = false
                };
            }

            var isMaintenanceStaff = await (
                from userRole in _context.UserRoles
                join role in _context.Roles on userRole.RoleId equals role.Id
                where userRole.UserId == maintenanceStaffUserId
                      && role.Name == UserRoles.MaintenanceStaff
                select userRole
            ).AnyAsync();

            if (!isMaintenanceStaff)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A kiválasztott felhasználó nem karbantartó.",
                    Data = false
                };
            }

            ticket.AssignedToId = maintenanceStaffUserId;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                IsSuccess = true,
                Message = "A hibajegy sikeresen hozzárendelve a karbantartóhoz.",
                Data = true
            };
        }
    }
}