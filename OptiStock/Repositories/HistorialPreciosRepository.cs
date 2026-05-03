using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialPrecios;

namespace OptiStock.Repositories
{
    public class HistorialPreciosRepository : IHistorialPreciosRepository
    {
        private readonly IMongoCollection<HistorialPrecios> _historialPrecios;

        public HistorialPreciosRepository(MongoDbContext context)
        {
            _historialPrecios = context.Database.GetCollection<HistorialPrecios>("HistorialPrecios");
        }

        public async Task<List<HistorialPrecios>> GetAllAsync()
        {
            return await _historialPrecios
                .Find(_ => true)
                .SortByDescending(historial => historial.FechaCambio)
                .ToListAsync();
        }

        public async Task<HistorialPrecios> GetByIdAsync(string id)
        {
            return await _historialPrecios
                .Find(historial => historial._id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(HistorialPreciosCreateDto dto)
        {
            var historial = new HistorialPrecios
            {
                IdEmpresa = dto.IdEmpresa,
                IdProducto = dto.IdProducto,
                CodigoProducto = dto.CodigoProducto,
                NombreProducto = dto.NombreProducto,
                PrecioAnterior = dto.PrecioAnterior,
                PrecioNuevo = dto.PrecioNuevo,
                Motivo = dto.Motivo,
                IdUsuario = dto.IdUsuario,
                NombreUsuario = dto.NombreUsuario,
                FechaCambio = DateTime.UtcNow
            };

            try
            {
                await _historialPrecios.InsertOneAsync(historial);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(HistorialPreciosUpdateDto dto, string id)
        {
            var filter = Builders<HistorialPrecios>.Filter.Eq(historial => historial._id, id);
            var update = Builders<HistorialPrecios>.Update
                .Set(historial => historial.Motivo, dto.Motivo)
                .Set(historial => historial.NombreUsuario, dto.NombreUsuario);
            try
            {
                var result = await _historialPrecios.UpdateOneAsync(filter, update);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var result = await _historialPrecios.DeleteOneAsync(historial => historial._id == id);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}