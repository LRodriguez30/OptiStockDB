using InterfazOptiStock.Models.Reportes;

namespace InterfazOptiStock.Schemas
{
    public class Reportes
    {
        public string? _id { get; set; }
        public required Guid IdEmpresa { get; set; }
        public required string TipoReporte { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public required Periodo Periodo { get; set; }
        public required Resumen Resumen { get; set; }
        public required Detalle Detalle { get; set; }
        public required Guid IdGeneradoPor { get; set; }
    }
}
