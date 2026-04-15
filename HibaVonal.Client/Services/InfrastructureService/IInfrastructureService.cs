using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;

namespace HibaVonal.Client.Services.InfrastructureService
{
    public interface IInfrastructureService
    {
        Task<ServiceResponse<List<EquipmentDTO>>> GetEquipmentsAsync();
    }
}
