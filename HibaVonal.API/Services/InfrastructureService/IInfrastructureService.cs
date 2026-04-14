using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.API.Services.InfrastructureService
{
    public interface IInfrastructureService
    {
        Task<ServiceResponse<List<EquipmentDTO>>> GetEquipmentsAsync();
    }
}
