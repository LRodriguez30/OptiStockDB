using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OptiStock.Models.Notificaciones;

namespace OptiStock.Schemas
{
    public class Notificaciones
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEmpresa { get; set; }
        public required string Tipo { get; set; }
        public required string Titulo { get; set; }
        public required string Mensaje { get; set; }
        public required decimal Prioridad { get; set; }
        public required decimal Modulo { get; set; }
        public required string IdReferencia { get; set; }
        public required bool Leida { get; set; } = false;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow.ToString("o") is string fecha ? DateTime.Parse(fecha) : DateTime.UtcNow;
        public DateTime FechaLectura { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid UsuarioDestino { get; set; }
        public Acciones? Acciones { get; set; }
    }
}
