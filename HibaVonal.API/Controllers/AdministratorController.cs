using HibaVonal.API.Data;
using HibaVonal.API.Models.Infrastructure;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HibaVonal.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.DEV}")]
    public class AdministratorController : ApiControllerBase
    {
        private readonly DataContext _context;

        public AdministratorController(DataContext context)
        {
            _context = context;
        }

        // Eszközök lekérése
        [HttpGet("equipments")]
        public async Task<ActionResult<List<EquipmentDTO>>> GetEquipments()
        {
            var equipments = await _context.Equipments
                .Select(e => new EquipmentDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type,
                    RoomNumber = e.RoomNumber
                }).ToListAsync();

            return Ok(equipments);
        }

        // Új eszköz hozzáadása
        [HttpPost("add")]
        public async Task<IActionResult> AddEquipment(EquipmentDTO equipmentDto)
        {
            var equipment = new Equipment
            {
                Name = equipmentDto.Name,
                Type = equipmentDto.Type,
                RoomNumber = equipmentDto.RoomNumber
            };

            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Eszköz törlése
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveEquipment(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null) return NotFound();

            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}