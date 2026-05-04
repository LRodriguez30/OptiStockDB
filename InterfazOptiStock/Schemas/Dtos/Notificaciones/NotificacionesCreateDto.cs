using InterfazOptiStock.Models.Notificaciones;

namespace InterfazOptiStock.Schemas.Dtos.Notificaciones
{
    public class NotificacionesCreateDto
    {
        public required Guid IdEmpresa { get; set; }
        public required string Tipo { get; set; }
        public required string Titulo { get; set; }
        public required string Mensaje { get; set; }
        public required string Prioridad { get; set; }
        public required string Modulo { get; set; }
        public required string IdReferencia { get; set; }
        public required bool Leida { get; set; } = false;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaLectura { get; set; }
        public required Guid UsuarioDestino { get; set; }
        public Acciones? Acciones { get; set; }
    }
}
