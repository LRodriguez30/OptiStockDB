namespace InterfazOptiStock.Models.Reportes
{
    public class Detalle
    {
        public required string IdProducto { get; set; }
        public required string NombreProducto { get; set; }
        public required int Cantidad { get; set; }
        public required decimal Total { get; set; }
    }
}
