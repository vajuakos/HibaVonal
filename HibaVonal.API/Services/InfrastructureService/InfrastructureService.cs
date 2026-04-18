using HibaVonal.API.Data;
using HibaVonal.API.Models.Infrastructure;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using HibaVonal.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.API.Services.InfrastructureService
{
    public class InfrastructureService : IInfrastructureService
    {
        private readonly DataContext _context;

        public InfrastructureService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<EquipmentDTO>>> GetEquipmentsAsync()
        {
            var equipments = await _context.Equipments
                .AsNoTracking()
                .Select(e => new EquipmentDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type,
                    RoomNumber = e.RoomNumber
                }).ToListAsync();
            
            return new ServiceResponse<List<EquipmentDTO>>
            {
                Data = equipments,
                IsSuccess = true,
                Message = "Eszközök sikeresen lekérve"
            };

        }

        public async Task<ServiceResponse<bool>> SubmitEquipmentRequestAsync(EquipmentRequestDTO dto, int currentUserId)
        {
            if (dto.TicketId <= 0)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Érvénytelen hibajegy lett megadva."
                };
            }

            if (dto.EquipmentId <= 0)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Érvénytelen eszköz lett megadva."
                };
            }

            if (dto.Quantity <= 0)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A mennyiségnek 0-nál nagyobbnak kell lennie."
                };
            }

            var ticket = await _context.MaintenanceTickets
                .FirstOrDefaultAsync(t => t.Id == dto.TicketId);

            if (ticket == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A kiválasztott hibajegy nem található."
                };
            }

            if (ticket.Status == TicketStatus.Resolved)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Lezárt hibajegyhez nem lehet eszközigényt leadni."
                };
            }

            var equipmentExists = await _context.Equipments
                .AnyAsync(e => e.Id == dto.EquipmentId);

            if (!equipmentExists)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "A kiválasztott eszköz nem található."
                };
            }

            var roleNames = await (
                from userRole in _context.UserRoles
                join role in _context.Roles on userRole.RoleId equals role.Id
                where userRole.UserId == currentUserId
                select role.Name!
            ).ToListAsync();

            var isMaintenanceStaff = roleNames.Contains(UserRoles.MaintenanceStaff);
            var isMaintenanceManager = roleNames.Contains(UserRoles.MaintenanceManager);
            var isDev = roleNames.Contains(UserRoles.DEV);

            if (!isMaintenanceStaff && !isMaintenanceManager && !isDev)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Nincs jogosultságod eszközigény leadásához."
                };
            }

            if (isMaintenanceStaff && !isMaintenanceManager && !isDev)
            {
                if (ticket.AssignedToId != currentUserId)
                {
                    return new ServiceResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Karbantartóként csak a hozzád rendelt hibajegyhez adhatsz le eszközigényt."
                    };
                }
            }

            var request = new EquipmentRequest
            {
                TicketId = dto.TicketId,
                EquipmentId = dto.EquipmentId,
                Quantity = dto.Quantity,
                RequestedById = currentUserId,
                RequestedAt = DateTime.UtcNow
            };

            _context.EquipmentRequests.Add(request);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                IsSuccess = true,
                Message = "Eszközigény sikeresen leadva!"
            };
        }
    }
}
