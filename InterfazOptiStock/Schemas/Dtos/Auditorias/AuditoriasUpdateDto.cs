namespace InterfazOptiStock.Schemas.Dtos.Auditorias
{
    public class AuditoriasUpdateDto
    {
        public object DatosAnteriores { get; set; } = null!;
        public object DatosNuevos { get; set; } = null!;
    }
}
