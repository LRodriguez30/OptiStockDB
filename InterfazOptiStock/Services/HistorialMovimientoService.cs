using System.Text;
using System.Text.Json;
using InterfazOptiStock.Schemas;
using InterfazOptiStock.Schemas.Dtos.HistorialMovimientos;

namespace InterfazOptiStock.Services
{
    public class HistorialMovimientosService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://localhost:5144/api/HistorialMovimientos";

        public HistorialMovimientosService()
        {
            _httpClient = new HttpClient();
        }

        // GET ALL
        public async Task<List<HistorialMovimientos>> GetHistorialMovimientosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<HistorialMovimientos>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<HistorialMovimientos>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<HistorialMovimientos>();
        }

        // GET BY ID
        public async Task<HistorialMovimientos?> GetHistorialMovimientoAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HistorialMovimientos>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // POST
        public async Task<HistorialMovimientos?> CreateHistorialMovimientoAsync(HistorialMovimientosCreateDto historialMovimiento)
        {
            var json = JsonSerializer.Serialize(historialMovimiento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HistorialMovimientos>(
                resultJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // PUT
        public async Task<bool> UpdateHistorialMovimientoAsync(string id, HistorialMovimientosUpdateDto historialMovimiento)
        {
            var json = JsonSerializer.Serialize(historialMovimiento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);

            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> DeleteHistorialMovimientoAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
