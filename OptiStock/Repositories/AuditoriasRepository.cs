using MongoDB.Bson;
using MongoDB.Driver;
using OptiStock.AppContext;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas;
using OptiStock.Schemas.Dtos.Auditorias;
using OptiStock.Schemas.Dtos.HistorialPrecios;
using System.Text.Json;

namespace OptiStock.Repositories
{
    public class AuditoriasRepository: IAuditoriasRepository
    {
        private readonly IMongoCollection<Auditorias> _auditorias;

        public AuditoriasRepository(MongoDbContext context)
        {
            _auditorias = context.Database.GetCollection<Auditorias>("Auditorias");
        }

        public async Task<List<AuditoriasDto>> GetAllAsync()
        {
            var data = await _auditorias
                .Find(_ => true)
                .SortByDescending(x => x.FechaEvento)
                .ToListAsync();

            return data.Select(auditoria => new AuditoriasDto
            {
                Id = auditoria._id,
                IdEmpresa = auditoria.IdEmpresa,
                IdUsuario = auditoria.IdUsuario,
                NombreUsuario = auditoria.NombreUsuario,
                Modulo = auditoria.Modulo,
                Accion = auditoria.Accion,
                Descripcion = auditoria.Descripcion,
                Entidad = auditoria.Entidad,
                IdEntidad = auditoria.IdEntidad,
                DatosAnteriores = auditoria.DatosAnteriores.ToDictionary(),
                DatosNuevos = auditoria.DatosNuevos.ToDictionary(),
                FechaEvento = auditoria.FechaEvento,
                NombreGrupo = auditoria.NombreGrupo,
                IdAutorizadoPor = auditoria.IdAutorizadoPor
            }).ToList();
        }

        public async Task<AuditoriasDto?> GetByIdAsync(string id)
        {
            var auditoria = await _auditorias
                .Find(auditoria => auditoria._id == id)
                .FirstOrDefaultAsync();
            
            if (auditoria is null)
                return null;

            return new AuditoriasDto
            {
                Id = auditoria._id,
                IdEmpresa = auditoria.IdEmpresa,
                IdUsuario = auditoria.IdUsuario,
                NombreUsuario = auditoria.NombreUsuario,
                Modulo = auditoria.Modulo,
                Accion = auditoria.Accion,
                Descripcion = auditoria.Descripcion,
                Entidad = auditoria.Entidad,
                IdEntidad = auditoria.IdEntidad,
                DatosAnteriores = auditoria.DatosAnteriores.ToDictionary(),
                DatosNuevos = auditoria.DatosNuevos.ToDictionary(),
                FechaEvento = auditoria.FechaEvento,
                NombreGrupo = auditoria.NombreGrupo,
                IdAutorizadoPor = auditoria.IdAutorizadoPor
            };
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
                DatosAnteriores = ConvertToBson(dto.DatosAnteriores),
                DatosNuevos = ConvertToBson(dto.DatosNuevos),
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
            if (!ObjectId.TryParse(id, out var objectId))
                return false;

            var exists = await _auditorias
                .Find(a => a._id == id)
                .AnyAsync();

            if (!exists)
                return false;

            var datosAnteriores = dto.DatosAnteriores switch
            {
                BsonDocument bson => bson,
                string json => BsonDocument.Parse(json),
                _ => dto.DatosAnteriores != null
                        ? BsonDocument.Parse(dto.DatosAnteriores.ToJson())
                        : new BsonDocument()
            };

            var datosNuevos = dto.DatosNuevos switch
            {
                BsonDocument bson => bson,
                string json => BsonDocument.Parse(json),
                _ => dto.DatosNuevos != null
                        ? BsonDocument.Parse(dto.DatosNuevos.ToJson())
                        : new BsonDocument()
            };

            var filter = Builders<Auditorias>.Filter.Eq(a => a._id, id);

            var update = Builders<Auditorias>.Update
                .Set(a => a.DatosAnteriores, datosAnteriores)
                .Set(a => a.DatosNuevos, datosNuevos);

            var result = await _auditorias.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }


        public async Task<bool> DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return false;

            try
            {
                var filter = Builders<Auditorias>.Filter.Eq(a => a._id, id);

                var result = await _auditorias.DeleteOneAsync(filter);

                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private static BsonDocument ConvertToBson(object obj)
        {
            if (obj is JsonElement jsonElement)
            {
                return BsonDocument.Parse(jsonElement.GetRawText());
            }

            return obj.ToBsonDocument();
        }
    }
}
