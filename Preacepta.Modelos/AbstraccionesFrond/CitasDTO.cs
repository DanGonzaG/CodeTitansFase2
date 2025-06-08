using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CitasDTO
    {
        public int IdCita { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int IdTipoCita { get; set; }
        public int? Anfitrion { get; set; }
        public string? LinkVideo { get; set; }

        // Campo adicional informativo (navegación)
        public string? NombreTipoCita { get; set; }

        public DateTime FechaHora => Fecha.ToDateTime (Hora);
    }
}