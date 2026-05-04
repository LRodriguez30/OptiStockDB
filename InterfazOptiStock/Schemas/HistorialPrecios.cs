namespace InterfazOptiStock.Schemas
{
    public class HistorialPrecios
    {
        public string? _id { get; set; }
        public required Guid IdEmpresa { get; set; }
        public required Guid IdProducto { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required decimal PrecioAnterior { get; set; }
        public required decimal PrecioNuevo { get; set; }
        public string? Motivo { get; set; }
        public required Guid IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public DateTime FechaCambio { get; set; }
    }
}
