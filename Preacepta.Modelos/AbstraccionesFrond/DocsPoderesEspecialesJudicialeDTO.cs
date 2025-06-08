using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsPoderesEspecialesJudicialeDTO
    {
        [Display(Name = "ID Documento")]
        public int IdDoc { get; set; }

        [Display(Name = "Fecha")]
        public string Fecha { get; set; }

        [Display(Name = "ID Abogado")]
        public int IdAbogado { get; set; }

        [Display(Name = "ID Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Texto del Documento")]
        public string Texto { get; set; } = null!;

        [Display(Name = "Navegación Abogado")]
        public virtual TGeAbogado IdAbogadoNavigation { get; set; } = null!;

        [Display(Name = "Navegación Cliente")]
        public virtual TGePersona IdClienteNavigation { get; set; } = null!;
    }
}
