using Preacepta.Modelos.AbstraccionesBD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class HistorialDocumentoDTO
    {
        [DisplayName("Identificador")]
        public int Id { get; set; }

        [DisplayName("Fecha")]
        public string? Fecha { get; set; } 

        [DisplayName("Cliente")]
        public int? Cliente { get; set; }

        [DisplayName("Abogado")]
        public int? Abogado { get; set; }

        [Required, DisplayName("Tipo de documento")]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres")]
        public string TipoDocumento { get; set; } = null!;

        [DisplayName("Id del documento")]
        public int IdDocumento { get; set; }

        [Required, DisplayName("Título")]
        [MaxLength(200, ErrorMessage = "El nombre no debe exceder los 200 caracteres")]
        public string Titulo { get; set; } = null!;

        public TGePersona? ClienteNavigation { get; set; }
        public TGeAbogado? AbogadoNavigation { get; set; }
    }
}
