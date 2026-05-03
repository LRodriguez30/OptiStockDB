using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OptiStock.Schemas.Dtos.HistorialPrecios
{
    public class HistorialPreciosCreateDto
    {
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEmpresa { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdProducto { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required decimal PrecioAnterior { get; set; }
        public required decimal PrecioNuevo { get; set; }
        public string? Motivo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow.ToString("o") is string fecha ? DateTime.Parse(fecha) : DateTime.UtcNow;
    }
}
