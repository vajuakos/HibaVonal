using HibaVonal.API.Data;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using HibaVonal.Shared.DTO.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
