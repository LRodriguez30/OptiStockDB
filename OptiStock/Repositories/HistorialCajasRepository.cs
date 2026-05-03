using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCajas;
using OptiStock.Schemas.Dtos.HistorialCostos;

namespace OptiStock.Repositories
{
    public class HistorialCajasRepository : IHistorialCajasRepository
    {
        private readonly IMongoCollection<HistorialCajas> _historialCajas;

        public HistorialCajasRepository(MongoDbContext context)
        {
            _historialCajas = context.Database.GetCollection<HistorialCajas>("HistorialCajas");
        }

        public async Task<List<HistorialCajas>> GetAllAsync()
        {
            return await _historialCajas
                .Find(_ => true)
                .SortByDescending(historial => historial.FechaCambio)
                .ToListAsync();
        }

        public async Task<HistorialCajas> GetByIdAsync(string id)
        {
            return await _historialCajas
                .Find(historial => historial._id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(HistorialCajasCreateDto dto)
        {
            var historial = new HistorialCajas
            {
                IdEmpresa = dto.IdEmpresa,
                IdCaja = dto.IdCaja,
                NombreCaja = dto.NombreCaja,
                IdTurno = dto.IdTurno,
                IdUsuario = dto.IdUsuario,
                TipoMovimiento = dto.TipoMovimiento,
                Concepto = dto.Concepto,
                Monto = dto.Monto,
                SaldoAnterior = dto.SaldoAnterior,
                SaldoNuevo = dto.SaldoNuevo,
                IdOperacion = dto.IdOperacion,
                FechaCambio = DateTime.UtcNow,
                Observaciones = dto.Observaciones
            };

            try
            {
                await _historialCajas.InsertOneAsync(historial);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(HistorialCajasUpdateDto dto, string id)
        {
            var update = Builders<HistorialCajas>.Update
                .Set(historial => historial.Observaciones, dto.Observaciones);

            var result = await _historialCajas.UpdateOneAsync(
                historial => historial._id == id,
                update
            );
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _historialCajas.DeleteOneAsync(historial => historial._id == id);
            return result.DeletedCount > 0;
        }
    }
}