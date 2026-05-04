using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OptiStock.Schemas
{
    public class HistorialCajas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEmpresa { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdCaja { get; set; }
        public required string NombreCaja { get; set; }
        public required string IdTurno { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdUsuario { get; set; }
        public required string TipoMovimiento { get; set; }
        public required string Concepto { get; set; }
        public required decimal Monto { get; set; }
        public required decimal SaldoAnterior { get; set; }
        public required decimal SaldoNuevo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdOperacion { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;
        public string? Observaciones { get; set; }
    }
}
