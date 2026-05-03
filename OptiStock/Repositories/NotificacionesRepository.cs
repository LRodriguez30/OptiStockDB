using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.HistorialPrecios;
using OptiStock.Schemas.Dtos.Notificaciones;

namespace OptiStock.Repositories
{
    public class NotificacionesRepository: INotificacionesRepository
    {
        private readonly IMongoCollection<Notificaciones> _notificaciones;

        public NotificacionesRepository(MongoDbContext context)
        {
            _notificaciones = context.Database.GetCollection<Notificaciones>("Notificaciones");
        }

        public async Task<List<Notificaciones>> GetAllAsync()
        {
            return await _notificaciones
                .Find(_ => true)
                .SortByDescending(notificacion => notificacion.FechaCreacion)
                .ToListAsync();
        }

        public async Task<Notificaciones> GetByIdAsync(string id)
        {
            return await _notificaciones
                .Find(notificacion => notificacion._id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(NotificacionesCreateDto dto)
        {
            var notificacion = new Notificaciones
            {
                IdEmpresa = dto.IdEmpresa,
                Tipo = dto.Tipo,
                Titulo = dto.Titulo,
                Mensaje = dto.Mensaje,
                Prioridad = dto.Prioridad,
                Modulo = dto.Modulo,
                IdReferencia = dto.IdReferencia,
                Leida = dto.Leida,
                FechaCreacion = DateTime.UtcNow,
                FechaLectura = dto.FechaLectura,
                UsuarioDestino = dto.UsuarioDestino,
                Acciones = dto.Acciones
            };

            try
            {
                await _notificaciones.InsertOneAsync(notificacion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(NotificacionesUpdateDto dto, string id)
        {
            var filter = Builders<Notificaciones>.Filter.Eq(notificacion => notificacion._id, id);
            var update = Builders<Notificaciones>.Update
                .Set(notificacion => notificacion.Leida, dto.Leida)
                .Set(notificacion => notificacion.Acciones, dto.Acciones);
            try
            {
                var result = await _notificaciones.UpdateOneAsync(filter, update);
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
                var result = await _notificaciones.DeleteOneAsync(historial => historial._id == id);
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
