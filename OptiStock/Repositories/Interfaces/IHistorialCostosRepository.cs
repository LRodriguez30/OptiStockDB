using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCostos;

namespace OptiStock.Repositories.Interfaces
{
    public interface IHistorialCostosRepository
    {
        public Task<List<HistorialCostos>> GetAllAsync();
        public Task<HistorialCostos> GetByIdAsync(string id);
        public Task<bool> CreateAsync(HistorialCostosCreateDto dto);
        public Task<bool> UpdateAsync(HistorialCostosUpdateDto dto, string id);
        public Task<bool> DeleteAsync(string id);
    }
}