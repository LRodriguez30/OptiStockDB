using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OptiStock.Schemas.Dtos.HistorialMovimientos
{
    public class HistorialMovimientosCreateDto
    {
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEmpresa { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdProducto { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required string TipoMovimiento { get; set; }
        public required int Cantidad { get; set; }
        public required int StockAnterior { get; set; }
        public required int StockNuevo { get; set; }
        public required string Concepto { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdDocumento { get; set; }
        public required int NumeroDocumento { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdUsuario { get; set; }
        public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow.ToString("o") is string fecha ? DateTime.Parse(fecha) : DateTime.UtcNow;
        public string? Observaciones { get; set; }
    }
}
