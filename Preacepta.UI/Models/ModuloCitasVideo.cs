using System.ComponentModel;

namespace Praecepta.UI.Models
{
    public class ModuloCitasVideo
    {
        [DisplayName("Numero de cita")]
        public int NumCita { get; set; }
        public string Evento { get; set; }

        [DisplayName("Fecha")]
        public DateTime FechaHora { get; set; }

        [DisplayName("Tipo")]
        public string tipo { get; set; }



    }
}
