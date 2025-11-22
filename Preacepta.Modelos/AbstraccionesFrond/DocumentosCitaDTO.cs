using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocumentosCitaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public int IdCita { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [MaxLength(255, ErrorMessage = "El nombre no debe exceder los 255 caracteres")]
        public string NombreArchivo { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        [MaxLength(500, ErrorMessage = "El nombre no debe exceder los 500 caracteres")]
        public string RutaArchivo { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public DateTime FechaSubida { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public bool Descargar { get; set; }

        [Required(ErrorMessage = "Dato requerido")]
        public bool Activo { get; set; }
    }
}
