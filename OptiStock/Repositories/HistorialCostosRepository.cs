using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialCostos;

namespace OptiStock.Repositories
{
    public class HistorialCostosRepository: IHistorialCostosRepository
    {
        private readonly IMongoCollection<HistorialCostos> _historialCostos;

        public HistorialCostosRepository(MongoDbContext context)
        {
            _historialCostos = context.Database.GetCollection<HistorialCostos>("HistorialCostos");
        }

        public async Task<List<HistorialCostos>> GetAllAsync()
        {
            return await _historialCostos
                .Find(_ => true)
                .SortByDescending(historial => historial.FechaCambio)
                .ToListAsync();
        }

        public async Task<HistorialCostos> GetByIdAsync(string id)
        {
            return await _historialCostos
                .Find(historial => historial._id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(HistorialCostosCreateDto dto)
        {
            var historial = new HistorialCostos
            {
                IdEmpresa = dto.IdEmpresa,
                IdProducto = dto.IdProducto,
                CodigoProducto = dto.CodigoProducto,
                NombreProducto = dto.NombreProducto,
                CostoAnterior = dto.CostoAnterior,
                CostoNuevo = dto.CostoNuevo,
                IdProveedor = dto.IdProveedor,
                NombreProveedor = dto.NombreProveedor,
                Motivo = dto.Motivo,
                IdUsuario = dto.IdUsuario,
                NombreUsuario = dto.NombreUsuario,
                FechaCambio = DateTime.UtcNow
            };

            try
            {
                await _historialCostos.InsertOneAsync(historial);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(HistorialCostosUpdateDto dto, string id)
        {
            var update = Builders<HistorialCostos>.Update
                .Set(historial => historial.NombreProveedor, dto.NombreProveedor)
                .Set(historial => historial.Motivo, dto.Motivo)
                .Set(historial => historial.NombreUsuario, dto.NombreUsuario);

            var result = await _historialCostos.UpdateOneAsync(
                historial => historial._id == id,
                update
            );
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _historialCostos.DeleteOneAsync(historial => historial._id == id);
            return result.DeletedCount > 0;
        }
    }
}
