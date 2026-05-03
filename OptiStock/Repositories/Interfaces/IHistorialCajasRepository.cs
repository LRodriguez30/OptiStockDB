using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCajas;
using OptiStock.Schemas.Dtos.HistorialCostos;

namespace OptiStock.Repositories.Interfaces
{
    public interface IHistorialCajasRepository
    {
        public Task<List<HistorialCajas>> GetAllAsync();
        public Task<HistorialCajas> GetByIdAsync(string id);
        public Task<bool> CreateAsync(HistorialCajasCreateDto dto);
        public Task<bool> UpdateAsync(HistorialCajasUpdateDto dto, string id);
        public Task<bool> DeleteAsync(string id);
    }
}
