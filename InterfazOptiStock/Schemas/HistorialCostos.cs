namespace InterfazOptiStock.Schemas
{
    public class HistorialCostos
    {
        public string? _id { get; set; }
        public required Guid IdEmpresa { get; set; }
        public required Guid IdProducto { get; set; }
        public required string CodigoProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required decimal CostoAnterior { get; set; }
        public required decimal CostoNuevo { get; set; }
        public required Guid IdProveedor { get; set; }
        public string? NombreProveedor { get; set; }
        public string? Motivo { get; set; }
        public required Guid IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public DateTime FechaCambio { get; set; }
    }
}
