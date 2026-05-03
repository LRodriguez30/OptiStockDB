using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCostos;
using OptiStock.Schemas.Dtos.HistorialMovimientos;

namespace OptiStock.Repositories
{
    public class HistorialMovimientosRepository : IHistorialMovimientosRepository
    {
        private readonly IMongoCollection<HistorialMovimientos> _historialMovimientos;

        public HistorialMovimientosRepository(MongoDbContext context)
        {
            _historialMovimientos = context.Database.GetCollection<HistorialMovimientos>("HistorialMovimientos");
        }

        public async Task<List<HistorialMovimientos>> GetAllAsync()
        {
            return await _historialMovimientos
                .Find(_ => true)
                .SortByDescending(historial => historial.FechaMovimiento)
                .ToListAsync();
        }

        public async Task<HistorialMovimientos> GetByIdAsync(string id)
        {
            return await _historialMovimientos
                .Find(historial => historial._id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(HistorialMovimientosCreateDto dto)
        {
            var historial = new HistorialMovimientos
            {
                IdEmpresa = dto.IdEmpresa,
                IdProducto = dto.IdProducto,
                CodigoProducto = dto.CodigoProducto,
                NombreProducto = dto.NombreProducto,
                TipoMovimiento = dto.TipoMovimiento,
                Cantidad = dto.Cantidad,
                StockAnterior = dto.StockAnterior,
                StockNuevo = dto.StockNuevo,
                Concepto = dto.Concepto,
                IdDocumento = dto.IdDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                IdUsuario = dto.IdUsuario,
                FechaMovimiento = DateTime.UtcNow,
                Observaciones = dto.Observaciones
            };

            try
            {
                await _historialMovimientos.InsertOneAsync(historial);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(HistorialMovimientosUpdateDto dto, string id)
        {
            var update = Builders<HistorialMovimientos>.Update
                .Set(historial => historial.Observaciones, dto.Observaciones);

            var result = await _historialMovimientos.UpdateOneAsync(
                historial => historial._id == id,
                update
            );
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _historialMovimientos.DeleteOneAsync(historial => historial._id == id);
            return result.DeletedCount > 0;
        }
    }
}