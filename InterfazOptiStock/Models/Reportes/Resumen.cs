namespace InterfazOptiStock.Models.Reportes
{
    public class Resumen
    {
        public required string VentasTotales { get; set; }
        public required int CantidadFaturas { get; set; }
        public required int ProductosVendidos { get; set; }
        public required int ClientesAtendidos { get; set; }
    }
}
