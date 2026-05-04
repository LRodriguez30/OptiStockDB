using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazOptiStock.Models.HistorialCajas
{
    public class PropiedadesHistorialCajas
    {
        public string? _id { get; set; }

        public Guid IdEmpresa { get; set; }
        public Guid IdCaja { get; set; }

        public string? NombreCaja { get; set; }
        public string? IdTurno { get; set; }

        public Guid IdUsuario { get; set; }

        public string? TipoMovimiento { get; set; }
        public string? Concepto { get; set; }

        public decimal Monto { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoNuevo { get; set; }

        public Guid IdOperacion { get; set; }

        public DateTime FechaCambio { get; set; }

        public string? Observaciones { get; set; }
    }
}
