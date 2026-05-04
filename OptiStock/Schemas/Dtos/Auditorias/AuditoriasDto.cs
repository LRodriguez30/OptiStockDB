namespace OptiStock.Schemas.Dtos.Auditorias
{
    public class AuditoriasDto
    {
        public string? Id { get; set; }

        public Guid IdEmpresa { get; set; }
        public Guid IdUsuario { get; set; }

        public string? NombreUsuario { get; set; }
        public string? Modulo { get; set; }
        public string? Accion { get; set; }
        public string? Descripcion { get; set; }
        public string? Entidad { get; set; }
        public Guid IdEntidad { get; set; }

        public object? DatosAnteriores { get; set; }
        public object? DatosNuevos { get; set; }

        public DateTime FechaEvento { get; set; }

        public string? NombreGrupo { get; set; }
        public Guid IdAutorizadoPor { get; set; }
    }
}
