using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OptiStock.Schemas.Dtos.Auditorias
{
    public class AuditoriasCreateDto
    {
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEmpresa { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdUsuario { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Modulo { get; set; }
        public required string Accion { get; set; }
        public required string Descripcion { get; set; }
        public required string Entidad { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEntidad { get; set; }
        public object DatosAnteriores { get; set; } = new();
        public object DatosNuevos { get; set; } = new();
        public DateTime FechaEvento { get; set; } = DateTime.UtcNow;
        public required string NombreGrupo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdAutorizadoPor { get; set; }
    }
}