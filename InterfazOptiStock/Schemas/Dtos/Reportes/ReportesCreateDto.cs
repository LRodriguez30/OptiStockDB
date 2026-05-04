using InterfazOptiStock.Models.Reportes;

namespace InterfazOptiStock.Schemas.Dtos.Reportes
{
    public class ReportesCreateDto
    {
        public required Guid IdEmpresa { get; set; }
        public required string TipoReporte { get; set; }
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public required Periodo Periodo { get; set; }
        public required Resumen Resumen { get; set; }
        public required Detalle Detalle { get; set; }
        public required Guid IdGeneradoPor { get; set; }
    }
}
