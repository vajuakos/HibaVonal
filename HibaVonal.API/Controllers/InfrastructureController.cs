using HibaVonal.API.Services.InfrastructureService;
using HibaVonal.API.Services.MaintenanceStaffService;
using HibaVonal.Shared.Constants;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using HibaVonal.Shared.DTO.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.API.Controllers
{
    [Route("api/infrastructure")]
    [ApiController]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.MaintenanceStaff}, {UserRoles.MaintenanceManager}, {UserRoles.DEV}")]

    public class InfrastructureController : ApiControllerBase
    {
        private readonly IInfrastructureService _infrastructureService;

        public InfrastructureController(IInfrastructureService infrastructureService)
        {
            _infrastructureService = infrastructureService;
        }

        [HttpGet("equipments")]
        public async Task<ServiceResponse<List<EquipmentDTO>>> GetEquipments()
        {
            return await _infrastructureService.GetEquipmentsAsync();
        }
    }
}
