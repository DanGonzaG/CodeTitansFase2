using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    [Table("HistorialDocumentos")]
    public class HistorialDocumento
    {
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoDocumento { get; set; }

        public string Cliente { get; set; }

        public string Abogado { get; set; }

        public int? DocumentoIdOriginal { get; set; }
    }
}
