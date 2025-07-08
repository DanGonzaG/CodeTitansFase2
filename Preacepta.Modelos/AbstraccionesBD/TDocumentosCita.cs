using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesBD
{
    public class TDocumentosCita
    {
        public int Id { get; set; }
        public int IdCita { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime FechaSubida { get; set; }
        public virtual TCita Cita { get; set; }
        public bool Descargar { get; set; } = true;

    }
}
