using HibaVonal.API.Models;
using HibaVonal.API.Models.Ticket;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.Enum;

namespace HibaVonal.API.Extensions
{
    public static class TicketMappingExtensions
    {
        public static MaintenanceTicket ToEntity(this TicketDTO dto, int currentUserId)
        {
            return new MaintenanceTicket
            {
                Title = dto.Title,
                Description = dto.Description,
                RoomNumber = dto.RoomNumber,
                Status = TicketStatus.New,
                CreatedAt = DateTime.Now,
                CreatedById = currentUserId,
                AssignedToId = null
            };
        }

        public static TicketDTO ToDto(this MaintenanceTicket entity)
        {
            return new TicketDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                RoomNumber = entity.RoomNumber,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
