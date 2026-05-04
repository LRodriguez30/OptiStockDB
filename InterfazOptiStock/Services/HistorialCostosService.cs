using System.Text;
using System.Text.Json;
using InterfazOptiStock.Schemas;
using InterfazOptiStock.Schemas.Dtos.HistorialCostos;

namespace InterfazOptiStock.Services
{
    public class HistorialCostosService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://localhost:5144/api/HistorialCostos";
        public HistorialCostosService()
        {
            _httpClient = new HttpClient();
        }

        // GET ALL
        public async Task<List<HistorialCostos>> GetHistorialCostosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<HistorialCostos>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<HistorialCostos>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<HistorialCostos>();
        }

        // GET BY ID
        public async Task<HistorialCostos?> GetHistorialCostoAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HistorialCostos>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // POST
        public async Task<HistorialCostos?> CreateHistorialCostoAsync(HistorialCostosCreateDto historialCosto)
        {
            var json = JsonSerializer.Serialize(historialCosto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HistorialCostos>(
                resultJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // PUT
        public async Task<bool> UpdateHistorialCostoAsync(string id, HistorialCostosUpdateDto historialCosto)
        {
            var json = JsonSerializer.Serialize(historialCosto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);

            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> DeleteHistorialCostoAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
