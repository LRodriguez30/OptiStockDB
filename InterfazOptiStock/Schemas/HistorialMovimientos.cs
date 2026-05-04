namespace InterfazOptiStock.Schemas
{
    public class HistorialMovimientos
    {
        public string? _id { get; set; }
        public required Guid IdEmpresa { get; set; }
        public required Guid IdProducto { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required string TipoMovimiento { get; set; }
        public required int Cantidad { get; set; }
        public required int StockAnterior { get; set; }
        public required int StockNuevo { get; set; }
        public required string Concepto { get; set; }
        public required Guid IdDocumento{ get; set; }
        public required int NumeroDocumento { get; set; }
        public required Guid IdUsuario { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string? Observaciones { get; set; }
    }
}
