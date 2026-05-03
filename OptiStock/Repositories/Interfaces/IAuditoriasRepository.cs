using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.Auditorias;
using OptiStock.Schemas.Dtos.HistorialPrecios;

namespace OptiStock.Repositories.Interfaces
{
    public interface IAuditoriasRepository
    {
        public Task<List<Auditorias>> GetAllAsync();
        public Task<Auditorias> GetByIdAsync(string id);
        public Task<bool> CreateAsync(AuditoriasCreateDto dto);
        public Task<bool> UpdateAsync(AuditoriasUpdateDto dto, string id);
        public Task<bool> DeleteAsync(string id);
    }
}