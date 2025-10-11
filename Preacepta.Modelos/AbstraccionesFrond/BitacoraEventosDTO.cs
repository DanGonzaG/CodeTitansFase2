using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class BitacoraEventosDTO
    {
        public int Id_evento { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public string Tabla_Afectada { get; set; }
        public string Accion { get; set; }
        public int Id_registro_afectado { get; set; }
        public string Stack_error { get; set; }
    }
}
