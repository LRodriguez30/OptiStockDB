namespace OptiStock.Models.Reportes
{
    public class Resumen
    {
        public required decimal VentasTotales { get; set; }
        public required int CantidadFaturas { get; set; }
        public required int ProductosVendidos { get; set; }
        public required int ClientesAtendidos { get; set; }
    }
}
