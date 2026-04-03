using HibaVonal.Shared.DTO.Infrastructure;

namespace HibaVonal.Client.Services.Administrator
{
    public interface IAdministratorService
    {
        Task<List<EquipmentDTO>> GetEquipmentsAsync();
        Task<bool> AddEquipmentAsync(EquipmentDTO equipment);
        Task<bool> RemoveEquipmentAsync(int id);
    }
}