using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCostos;
using OptiStock.Schemas.Dtos.Notificaciones;

namespace OptiStock.Repositories.Interfaces
{
    public interface INotificacionesRepository
    {
        public Task<List<Notificaciones>> GetAllAsync();
        public Task<Notificaciones> GetByIdAsync(string id);
        public Task<bool> CreateAsync(NotificacionesCreateDto dto);
        public Task<bool> UpdateAsync(NotificacionesUpdateDto dto, string id);
        public Task<bool> DeleteAsync(string id);
    }
}
