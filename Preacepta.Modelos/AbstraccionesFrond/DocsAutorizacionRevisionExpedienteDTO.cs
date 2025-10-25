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
        [MaxLength(50, ErrorMessage = "Solo puede ingresar 50 caracteres")]
        public string Expediente { get; set; } = null!;

        [DisplayName("Delito")]
        [Required(ErrorMessage = "Debe especificar el delito")]
        [MaxLength(100, ErrorMessage = "Solo puede ingresar 100 caracteres")]
        public string Delito { get; set; } = null!;

        [Required(ErrorMessage = "Debe de elegir un imputado")]
        [DisplayName("Cédula del imputado")]
        public int CedulaImputado { get; set; }

        [DisplayName("Ofendido")]
        [Required(ErrorMessage = "Debe especificar el nombre del ofendido")]
        [MaxLength(150, ErrorMessage = "Solo puede ingresar 150 caracteres")]
        public string Ofendido { get; set; } = null!;

        [Required(ErrorMessage = "Debe de seleccionar un abogado")]
        [DisplayName("Cédula del abogado")]
        public int CedulaAbogado { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar un abogado")]
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
