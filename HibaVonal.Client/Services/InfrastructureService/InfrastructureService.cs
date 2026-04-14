using HibaVonal.Client.Services.MaintenanceStaffService;
using HibaVonal.Shared.DTO;
using HibaVonal.Shared.DTO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Client.Services.InfrastructureService
{
    public class InfrastructureService : IInfrastructureService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "api/infrastructure";

        public InfrastructureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<EquipmentDTO>>> GetEquipmentsAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<EquipmentDTO>>>(
                    $"{BaseUrl}/equipments");

                return result ?? new ServiceResponse<List<EquipmentDTO>>
                {
                    IsSuccess = false,
                    Message = "Nem érkezett válasz a szervertől."
                };
            }
            catch
            {
                return new ServiceResponse<List<EquipmentDTO>>
                {
                    IsSuccess = false,
                    Message = "Hiba történt az eszközök lekérésekor."
                };
            }
        }
    }
}
