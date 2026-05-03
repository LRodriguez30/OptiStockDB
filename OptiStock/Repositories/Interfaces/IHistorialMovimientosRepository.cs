using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCostos;
using OptiStock.Schemas.Dtos.HistorialMovimientos;

namespace OptiStock.Repositories.Interfaces
{
    public interface IHistorialMovimientosRepository
    {
        public Task<List<HistorialMovimientos>> GetAllAsync();
        public Task<HistorialMovimientos> GetByIdAsync(string id);
        public Task<bool> CreateAsync(HistorialMovimientosCreateDto dto);
        public Task<bool> UpdateAsync(HistorialMovimientosUpdateDto dto, string id);
        public Task<bool> DeleteAsync(string id);
    }
}
