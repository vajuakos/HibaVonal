using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;

namespace HibaVonal.API.Services.EquipmentRequestService
{
    public interface IEquipmentRequestService
    {
        Task<ServiceResponse<bool>> SubmitEquipmentRequestAsync(EquipmentRequestDTO dto, int currentUserId);
    }
}