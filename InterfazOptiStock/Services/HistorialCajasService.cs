using System.Text;
using System.Text.Json;
using InterfazOptiStock.Schemas;
using InterfazOptiStock.Schemas.Dtos.HistorialCajas;

namespace InterfazOptiStock.Services
{
    public class HistorialCajasService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://localhost:5144/api/HistorialCajas";

        public HistorialCajasService()
        {
            _httpClient = new HttpClient();
        }

        // GET ALL
        public async Task<List<HistorialCajas>> GetHistorialCajasAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<HistorialCajas>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<HistorialCajas>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<HistorialCajas>();
        }

        // GET BY ID
        public async Task<HistorialCajas?> GetHistorialCajaAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<HistorialCajas>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // POST
        public async Task<HistorialCajas?> CreateHistorialCajaAsync(HistorialCajasCreateDto historialCaja)
        {
            var json = JsonSerializer.Serialize(historialCaja);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultJson = await response.Content.ReadAsStringAsync();

            MessageBox.Show("Yes");
            return JsonSerializer.Deserialize<HistorialCajas>(
                resultJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // PUT
        public async Task<bool> UpdateHistorialCajaAsync(string id, HistorialCajasUpdateDto historialCaja)
        {
            var json = JsonSerializer.Serialize(historialCaja);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);

            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> DeleteHistorialCajaAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
