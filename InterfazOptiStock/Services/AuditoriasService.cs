using System.Text;
using System.Text.Json;
using InterfazOptiStock.Schemas;
using InterfazOptiStock.Schemas.Dtos.Auditorias;

namespace InterfazOptiStock.Services
{
    public class AuditoriasService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://localhost:5144/api/Auditorias";

        public AuditoriasService()
        {
            _httpClient = new HttpClient();
        }

        // GET ALL
        public async Task<List<Auditorias>> GetAuditoriasAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<Auditorias>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Auditorias>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<Auditorias>();
        }

        // GET BY ID
        public async Task<Auditorias?> GetAuditoriaAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Auditorias>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // POST
        public async Task<Auditorias?> CreateAuditoriaAsync(AuditoriasCreateDto auditoria)
        {
            var json = JsonSerializer.Serialize(auditoria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Auditorias>(
                resultJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // PUT
        public async Task<bool> UpdateAuditoriaAsync(string id, AuditoriasUpdateDto auditoria)
        {
            var json = JsonSerializer.Serialize(auditoria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);

            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> DeleteAuditoriaAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
