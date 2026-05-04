namespace InterfazOptiStock.Schemas
{
    public class HistorialCajas
    {
        public string? _id { get; set; }
        public required Guid IdEmpresa { get; set; }
        public required Guid IdCaja { get; set; }
        public required string NombreCaja { get; set; }
        public required string IdTurno { get; set; }
        public required Guid IdUsuario { get; set; }
        public required string TipoMovimiento { get; set; }
        public required string Concepto { get; set; }
        public required decimal Monto { get; set; }
        public required decimal SaldoAnterior { get; set; }
        public required decimal SaldoNuevo { get; set; }
        public required Guid IdOperacion { get; set; }
        public DateTime FechaCambio { get; set; }
        public string? Observaciones { get; set; }
    }
}
