using Preacepta.Modelos.AbstraccionesBD;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Preacepta.Modelos.AbstraccionesFrond
{
    public class GeAbogadoTipoDTO
    {
        [DisplayName("Id Tipo de abagado")]
        public int IdTipoAbogado { get; set; }

        [Required(ErrorMessage = "Debe de agregar nombre de la nueva especialdad del abogado")]
        public string Nombre { get; set; } = null!;
        
        public virtual ICollection<TGeAbogado> TGeAbogados { get; set; } = new List<TGeAbogado>();
    }
}
