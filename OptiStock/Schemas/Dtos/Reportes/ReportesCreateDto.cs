using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OptiStock.Models.Reportes;

namespace OptiStock.Schemas.Dtos.Reportes
{
    public class ReportesCreateDto
    {
        [BsonRepresentation(BsonType.String)]
        public required Guid IdEmpresa { get; set; }
        public required string TipoReporte { get; set; }
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow.ToString("o") is string fecha ? DateTime.Parse(fecha) : DateTime.UtcNow;
        public required Periodo Periodo { get; set; }
        public required Resumen Resumen { get; set; }
        public required Detalle Detalle { get; set; }
        [BsonRepresentation(BsonType.String)]
        public required Guid IdGeneradoPor { get; set; }
    }
}
