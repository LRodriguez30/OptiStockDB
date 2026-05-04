namespace InterfazOptiStock.Schemas
{
    public class Auditorias
    {
        public string? _id { get; set; }
        public required Guid IdEmpresa { get; set; }
        public required Guid IdUsuario { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Modulo { get; set; }
        public required string Accion { get; set; }
        public required string Descripcion { get; set; }
        public required string Entidad { get; set; }
        public required Guid IdEntidad { get; set; }
        public object? DatosAnteriores { get; set; }
        public object? DatosNuevos { get; set; }
        public DateTime FechaEvento { get; set; }
        public required string NombreGrupo { get; set; }
        public required Guid IdAutorizadoPor { get; set; }
    }
}
