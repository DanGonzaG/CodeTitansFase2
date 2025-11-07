using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesBD
{
    [Table("T_BitacoraEventos")]
    public partial class TBitacoraEventos
    {
        [Key]
        [Column("Id_evento")]
        public int Id_evento { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public string Tabla_Afectada { get; set; }
        public string Accion {  get; set; }
        public int Id_registro_afectado { get; set; }
        public string Stack_error { get; set; }
    }
}