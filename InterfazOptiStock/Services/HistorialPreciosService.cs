using System.Text;
using System.Text.Json;
using InterfazOptiStock.Schemas;
using InterfazOptiStock.Schemas.Dtos.HistorialPrecios;

namespace InterfazOptiStock.Services
{
    public class HistorialPreciosService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://localhost:5144/api/HistorialPrecios";
        public HistorialPreciosService()
        {
            _httpClient = new HttpClient();
        }

        // GET ALL
        public async Task<List<HistorialPrecios>> GetHistorialPreciosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<HistorialPrecios>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<HistorialPrecios>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<HistorialPrecios>();
        }

        // GET BY ID
        public async Task<HistorialPrecios?> GetHistorialPrecioAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HistorialPrecios>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // POST
        public async Task<HistorialPrecios?> CreateHistorialPrecioAsync(HistorialPreciosCreateDto historialPrecio)
        {
            var json = JsonSerializer.Serialize(historialPrecio);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HistorialPrecios>(
                resultJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // PUT
        public async Task<bool> UpdateHistorialPrecioAsync(string id, HistorialPreciosUpdateDto historialPrecio)
        {
            var json = JsonSerializer.Serialize(historialPrecio);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);

            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> DeleteHistorialPrecioAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
