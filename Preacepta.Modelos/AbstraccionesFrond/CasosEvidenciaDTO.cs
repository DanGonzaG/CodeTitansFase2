using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasosEvidenciaDTO
    {
        public int IdEvidencia { get; set; }

        [Required(ErrorMessage = "Debe de agregar un titulo a la evidencia")]
        [DisplayName("Titulo del caso")]
        public string Titulo { get; set; } = null!;
        [DisplayName("Documento")]
        public int IdCaso { get; set; }

        public string? Archivo { get; set; }

        [DisplayName("Nombre del caso")]
        public virtual TCasosEtapa IdCasoEtapa { get; set; } = null!;

        [DisplayName("Id Etapa PL")]
        public virtual TCaso IdCasoNavigation { get; set; } = null!;
    }
}
