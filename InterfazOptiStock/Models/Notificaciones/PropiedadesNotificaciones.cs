using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazOptiStock.Models.Notificaciones
{
    public class PropiedadesNotificaciones
    {
        public string? _id { get; set; }
        public Guid IdEmpresa { get; set; }
        public string? Tipo { get; set; }
        public string? Titulo { get; set; }
        public string? Mensaje { get; set; }
        public string? Prioridad { get; set; }
        public string? Modulo { get; set; }
        public string? IdReferencia { get; set; }
        public bool Leida { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaLectura { get; set; }
        public Guid UsuarioDestino { get; set; }
        public Acciones? Acciones { get; set; }
    }
}
