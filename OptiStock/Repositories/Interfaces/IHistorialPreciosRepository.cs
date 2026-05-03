using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialPrecios;

namespace OptiStock.Repositories.Interfaces
{
    public interface IHistorialPreciosRepository
    {
        public Task<List<HistorialPrecios>> GetAllAsync();
        public Task<HistorialPrecios> GetByIdAsync(string id);
        public Task<bool> CreateAsync(HistorialPreciosCreateDto dto);
        public Task<bool> UpdateAsync(HistorialPreciosUpdateDto dto, string id);
        public Task<bool> DeleteAsync(string id);
    }
}
