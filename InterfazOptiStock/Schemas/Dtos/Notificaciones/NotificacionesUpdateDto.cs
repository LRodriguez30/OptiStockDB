using InterfazOptiStock.Models.Notificaciones;

namespace InterfazOptiStock.Schemas.Dtos.Notificaciones
{
    public class NotificacionesUpdateDto
    {
        public required bool Leida { get; set; } = true;
        public Acciones? Acciones { get; set; }
    }
}
