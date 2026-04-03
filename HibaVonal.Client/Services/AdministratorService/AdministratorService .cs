
using HibaVonal.Shared.DTO;
using System.Net.Http.Json;

namespace HibaVonal.Client.Services.Administrator
{
    public class AdministratorService : IAdministratorService
    {
        private readonly HttpClient _http;

        public AdministratorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EquipmentDTO>> GetEquipmentsAsync()
        {
            return await _http.GetFromJsonAsync<List<EquipmentDTO>>("api/admin/equipments");
        }

        public async Task<bool> AddEquipmentAsync(EquipmentDTO equipment)
        {
            var response = await _http.PostAsJsonAsync("api/admin/add", equipment);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveEquipmentAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/admin/remove/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}