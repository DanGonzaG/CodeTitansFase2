using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class CasosEtapaDTO
    {
        [DisplayName("Id Etapa PL")]
        public int IdEtapaPl { get; set; }

        public string Fecha { get; set; }

        [Required(ErrorMessage = "Debe de agregar un titulo a la etapa del proceso legal")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Debe de agregar una descripción de la etapa del proceso legal")]        
        public string Descripcion { get; set; } = null!;

        [DisplayName("Id Caso")]
        
        public int IdCaso { get; set; }

        public bool Activo { get; set; }

        [DisplayName("Nombre Caso")]
        public virtual TCaso IdCasoNavigation { get; set; } = null!;
        
        public virtual ICollection<TCasosEvidencia> TCasosEvidencia { get; set; } = new List<TCasosEvidencia>();
    }
}

