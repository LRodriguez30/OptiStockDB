using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.Auditorias;
using OptiStock.Schemas.Dtos.HistorialPrecios;

namespace OptiStock.Repositories
{
    public class AuditoriasRepository: IAuditoriasRepository
    {
        private readonly IMongoCollection<Auditorias> _auditorias;

        public AuditoriasRepository(MongoDbContext context)
        {
            _auditorias = context.Database.GetCollection<Auditorias>("Auditorias");
        }

        public async Task<List<Auditorias>> GetAllAsync()
        {
            return await _auditorias
                .Find(_ => true)
                .SortByDescending(auditoria => auditoria.FechaEvento)
                .ToListAsync();
        }

        public async Task<Auditorias> GetByIdAsync(string id)
        {
            return await _auditorias
                .Find(auditoria => auditoria._id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(AuditoriasCreateDto dto)
        {
            var auditoria = new Auditorias
            {
                IdEmpresa = dto.IdEmpresa,
                IdUsuario = dto.IdUsuario,
                NombreUsuario = dto.NombreUsuario,
                Modulo = dto.Modulo,
                Accion = dto.Accion,
                Descripcion = dto.Descripcion,
                Entidad = dto.Entidad,
                IdEntidad = dto.IdEntidad,
                DatosAnteriores = dto.DatosAnteriores,
                DatosNuevos = dto.DatosNuevos,
                FechaEvento = DateTime.UtcNow,
                NombreGrupo = dto.NombreGrupo,
                IdAutorizadoPor = dto.IdAutorizadoPor
            };

            try
            {
                await _auditorias.InsertOneAsync(auditoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(AuditoriasUpdateDto dto, string id)
        {
            var filter = Builders<Auditorias>.Filter.Eq(auditoria => auditoria._id, id);
            var update = Builders<Auditorias>.Update
                .Set(auditoria => auditoria.DatosAnteriores, dto.DatosAnteriores)
                .Set(auditoria => auditoria.DatosNuevos, dto.DatosNuevos);
            
            try
            {
                var result = await _auditorias.UpdateOneAsync(filter, update);
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
                var result = await _auditorias.DeleteOneAsync(historial => historial._id == id);
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
