using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class DocsAutorizacionRevisionExpedienteDTO
    {
        [DisplayName("ID del Documento")]
        public int IdDocumento { get; set; }

        [DisplayName("Expediente")]
        [Required(ErrorMessage = "El campo 'Expediente' es obligatorio")]
        [StringLength(50)]
        public string Expediente { get; set; } = null!;

        [DisplayName("Delito")]
        [Required(ErrorMessage = "Debe especificar el delito")]
        [StringLength(100)]
        public string Delito { get; set; } = null!;

        [DisplayName("Cédula del imputado")]
        public int CedulaImputado { get; set; }

        [DisplayName("Ofendido")]
        [Required(ErrorMessage = "Debe especificar el nombre del ofendido")]
        [StringLength(150)]
        public string Ofendido { get; set; } = null!;

        [DisplayName("Cédula del abogado")]
        public int CedulaAbogado { get; set; }

        [DisplayName("Cédula del asistente")]
        public int CedulaAsistente { get; set; }

        [DisplayName("Abogado")]
        public virtual TGeAbogado? CedulaAbogadoNavigation { get; set; } = null!;

        [DisplayName("Asistente")]
        public virtual TGePersona? CedulaAsistenteNavigation { get; set; } = null!;

        [DisplayName("Imputado")]
        public virtual TGePersona? CedulaImputadoNavigation { get; set; } = null!;
    }
}
