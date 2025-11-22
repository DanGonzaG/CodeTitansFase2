using Microsoft.AspNetCore.Mvc.Rendering;
using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CitasDTO
    {
        
        public int IdCita { get; set; }

        [Required (ErrorMessage = "Fecha de la cita es obligatoria")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "Hora de la cita es obligatoria")]
        public TimeOnly Hora { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de cita")]
        public int? IdTipoCita { get; set; }

        [Required(ErrorMessage = "Elija un afitrion para la cita")]
        public int Anfitrion { get; set; }

        [MaxLength(100000, ErrorMessage = "El valor máximo es de 100000 letras")]
        public string? LinkVideo { get; set; }
        public int Estado { get; set; }
        public virtual TGeAbogado? AnfitrionNavigation { get; set; } = null!;


        public virtual TCitasTipo? IdTipoCitaNavigation { get; set; } = null!;


        public virtual ICollection<TCitasCliente>? TCitasClientes { get; set; } = new List<TCitasCliente>();

       
        public string? NombreTipoCita { get; set; }

        public string? NombreAnfitrion { get; set; }


        public DateTime FechaHora => Fecha.ToDateTime (Hora);

        public IEnumerable<SelectListItem>? TiposDeCita { get; set; }

        public int? IdCliente { get; set; }
        public List<string>? NombresClientes { get; set; }

        public List<DocumentosCitaDTO>? Documentos { get; set; }

    }
}