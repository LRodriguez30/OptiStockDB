using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCostos;
using OptiStock.Schemas.Dtos.Reportes;

namespace OptiStock.Repositories
{
    public class ReportesRepository: IReportesRepository
    {
        private readonly IMongoCollection<Reportes> _reportes;

        public ReportesRepository(MongoDbContext context)
        {
            _reportes = context.Database.GetCollection<Reportes>("Reportes");
        }

        public async Task<List<Reportes>> GetAllAsync()
        {
            return await _reportes
                .Find(_ => true)
                .SortByDescending(reporte => reporte.FechaGeneracion)
                .ToListAsync();
        }

        public async Task<Reportes> GetByIdAsync(string id)
        {
            return await _reportes
                .Find(reporte => reporte._id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(ReportesCreateDto dto)
        {
            var historial = new Reportes
            {
                IdEmpresa = dto.IdEmpresa,
                TipoReporte = dto.TipoReporte,
                FechaGeneracion = DateTime.UtcNow,
                Periodo = dto.Periodo,
                Resumen = dto.Resumen,
                Detalle = dto.Detalle,
                IdGeneradoPor = dto.IdGeneradoPor
            };

            try
            {
                await _reportes.InsertOneAsync(historial);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _reportes.DeleteOneAsync(historial => historial._id == id);
            return result.DeletedCount > 0;
        }
    }
}
