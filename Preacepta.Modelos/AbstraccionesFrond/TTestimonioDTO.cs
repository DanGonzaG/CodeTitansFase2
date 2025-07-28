using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class TTestimonioDTO
    {
        [DisplayName("Id del testimonio")]
        [Required(ErrorMessage = "Dato requerido")]
        public int IdTestimonio { get; set; }

        public string? Fecha { get; set; }

        [DisplayName("Id del cliente")]
        [Required(ErrorMessage = "Dato requerido")]
        public int IdCliente { get; set; }

        [MaxLength(500, ErrorMessage = "Capacidad del comentario excedida")]
        public string? Comentario { get; set; }

        public int? Evaluacion { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public bool Activo { get; set; }

        [DisplayName("Id del cliente en navegacion")]
        public virtual TGePersona? IdClienteNavigation { get; set; }
    }
}
