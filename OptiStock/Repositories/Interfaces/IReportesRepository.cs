using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCostos;
using OptiStock.Schemas.Dtos.Reportes;

namespace OptiStock.Repositories.Interfaces
{
    public interface IReportesRepository
    {
        public Task<List<Reportes>> GetAllAsync();
        public Task<Reportes> GetByIdAsync(string id);
        public Task<bool> CreateAsync(ReportesCreateDto dto);
        public Task<bool> DeleteAsync(string id);
    }
}
