using System.Text;
using System.Text.Json;
using InterfazOptiStock.Schemas;
using InterfazOptiStock.Schemas.Dtos.Notificaciones;

namespace InterfazOptiStock.Services
{
    public class NotificacionesService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "http://localhost:5144/api/Notificaciones";
        public NotificacionesService()
        {
            _httpClient = new HttpClient();
        }

        // GET ALL
        public async Task<List<Notificaciones>> GetNotificacionesAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<Notificaciones>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Notificaciones>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<Notificaciones>();
        }

        // GET BY ID
        public async Task<Notificaciones?> GetNotificacionAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Notificaciones>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // POST
        public async Task<Notificaciones?> CreateNotificacionAsync(NotificacionesCreateDto notificacion)
        {
            var json = JsonSerializer.Serialize(notificacion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Notificaciones>(
                resultJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        // PUT
        public async Task<bool> UpdateNotificacionAsync(string id, NotificacionesUpdateDto notificacion)
        {
            var json = JsonSerializer.Serialize(notificacion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);

            return response.IsSuccessStatusCode;
        }

        // DELETE
        public async Task<bool> DeleteNotificacionAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
