using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;

namespace HibaVonal.Client.Services.EquipmentRequestService
{
    public interface IEquipmentRequestService
    {
        Task<ServiceResponse<bool>> SubmitEquipmentRequestAsync(EquipmentRequestDTO dto);
    }
}