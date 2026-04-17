using HibaVonal.API.Services.EquipmentRequestService;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HibaVonal.API.Controllers
{
    [Route("api/equipmentRequest")]
    [ApiController]
    [Authorize(Roles = $"{UserRoles.MaintenanceStaff},{UserRoles.MaintenanceManager},{UserRoles.DEV}")]
    public class EquipmentRequestController : ApiControllerBase
    {
        private readonly IEquipmentRequestService _equipmentRequestService;

        public EquipmentRequestController(IEquipmentRequestService equipmentRequestService)
        {
            _equipmentRequestService = equipmentRequestService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> SubmitEquipmentRequest([FromBody] EquipmentRequestDTO dto)
        {
            var result = await _equipmentRequestService.SubmitEquipmentRequestAsync(dto, CurrentUserId);
            return Ok(result);
        }
    }
}