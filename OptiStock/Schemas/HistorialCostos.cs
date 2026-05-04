using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OptiStock.Schemas
{
    public class HistorialCostos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEmpresa { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdProducto { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required decimal CostoAnterior { get; set; }
        public required decimal CostoNuevo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdProveedor { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Motivo { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;
    }
}
